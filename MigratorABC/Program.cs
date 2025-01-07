using Confluent.Kafka;
using Elasticsearch.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MigratorABC;
using MigratorABC.Dto;
using MigratorABC.Events;
using MigratorProduct;
using MigratorProduct.Entities;
using Nest;
using Nest.JsonNetSerializer;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        // Build the host
        var host = CreateHostBuilder(args).Build();

        // Run the main application logic
        //await RunAsync(host);

        ////await GenerateMessagingTest(1200);

    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

                // Explicitly build the configuration
                var builtConfig = config.Build();

                // Access configuration values after build
                var data = builtConfig.GetSection("AppConfig");


            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<AppConfig>(context.Configuration.GetSection("AppConfig"));
                var config = context.Configuration.GetSection("AppConfig").Get<AppConfig>();

                // Register PostgreSQL DbContext
                services.AddDbContext<AppDbContext>(options =>
                {
                    var connectionString = config.POSTGRES_URL;
                    options.UseNpgsql(connectionString);
                });

                // Enable legacy timestamp behavior for PostgreSQL
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

                services.AddSingleton<IElasticClient>(provider =>
                {
                    var elasticsearchUrls = config.ELASTIC_URL.Split(',');
                    var connectionPool = new StaticConnectionPool(elasticsearchUrls.Select(url => new Uri(url)));
                    var connectionSettings = new ConnectionSettings(connectionPool, JsonNetSerializer.Default)
                        .DisableDirectStreaming();
                    return new ElasticClient(connectionSettings);
                });

                // Register custom services
                services.AddTransient<DataImport>();
                services.AddTransient<DataExport>();
            });

    static async Task RunAsync(IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var elasticClient = services.GetRequiredService<IElasticClient>();
            var dbContext = services.GetRequiredService<AppDbContext>();
            var dataExport = services.GetRequiredService<DataExport>();
            var dataImport = new DataImport(dbContext);

            // Perform Elasticsearch operations
            // var allPrices = await GetAllPriceAsync(dbContext, elasticClient);
            var allProduct = await dataExport.GetAllProductAsync();
            //var allOptionGroup = await dataExport.GetAllOptionGroupAsync();

            //await dataImport.BulkInsertProductPrice(allPrices);
            await dataImport.BulkInsertProduct(allProduct);
            //await dataImport.BulkInsertOptionGroup(allOptionGroup);



            Console.WriteLine("Data export and import completed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static async Task<List<ProductPriceDto>> GetAllPriceAsync(AppDbContext dbContext, IElasticClient elasticClient)
    {
        try
        {
            var allProducts = new List<ProductPriceDto>();
            const int batchSize = 10000; // Elasticsearch limit for batch size
            const string scrollTime = "15m"; // Keep scroll context alive for 15 minutes

            // Build the host to resolve services
            var exportData = new DataImport(dbContext);

            // Step 1: Initial scroll search request
            var searchResponse = await elasticClient.SearchAsync<ProductPriceDto>(s => s
                .Index("product_price")
                .Size(batchSize)
                .Scroll(scrollTime)
                .Query(q => q.MatchAll())
            );

            if (!searchResponse.IsValid || searchResponse.Documents.Count == 0)
            {
                throw new Exception($"Error starting scroll: {searchResponse.DebugInformation}");
            }

            // Extract the initial scroll ID
            string scrollId = searchResponse.ScrollId;

            do
            {
                // Process the current batch of documents
                await exportData.BulkInsertProductPrice(searchResponse.Documents.ToList());
                Console.WriteLine($"Processed {searchResponse.Documents.Count} records...");

                // Add to the total list (optional, only needed if you need all records in memory)
                allProducts.AddRange(searchResponse.Documents);

                // Step 2: Fetch the next batch using the scroll ID
                searchResponse = await elasticClient.ScrollAsync<ProductPriceDto>(scrollTime, scrollId);

                if (!searchResponse.IsValid)
                {
                    throw new Exception($"Error during scroll: {searchResponse.DebugInformation}");
                }

                // Update the scroll ID for the next request
                scrollId = searchResponse.ScrollId;

            } while (searchResponse.Documents.Any());

            // Step 3: Clear the scroll context to free resources
            await elasticClient.ClearScrollAsync(cs => cs.ScrollId(scrollId));

            Console.WriteLine($"Total records retrieved and processed: {allProducts.Count}");
            return allProducts;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while fetching data: {ex.Message}", ex);
        }
    }

    public static async Task GenerateMessagingTest(int totalMessage = 50)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "host.docker.internal:19092",
            AllowAutoCreateTopics = true,
        };  

        var serializerOptions = new JsonSerializerOptions { WriteIndented = false };
        var currentDate = DateTime.Now.ToString("ddMMyyyy");

        using var producer = new ProducerBuilder<string, string>(config).Build();

        var tasks = new List<Task>();

        for (int index = 0; index < totalMessage; index++)
        {
            string messageKey = $"XYZ21{index}_F100_{index}_{currentDate}";
            string article = GenerateCharacters(8);
            string ahNode = $"F100{GenerateCharacters(8)}";
            string validFrom = DateTime.Now.ToString("dd.MM.yyyy");
            string validTo = DateTime.Now.AddMonths(3).ToString("dd.MM.yyyy");

            Console.WriteLine("sent!");

            // Add tasks for each event
            tasks.Add(SendEvent(producer, "product_ah", messageKey, CreateProductAHMessage(messageKey, article, ahNode), serializerOptions));
            tasks.Add(SendEvent(producer, "product_listing", messageKey, CreateProductListingMessage(messageKey, article, GenerateCharacters(4)), serializerOptions));
            tasks.Add(SendEvent(producer, "product_listing", messageKey, CreateProductListingMessage(messageKey, article, GenerateCharacters(4)), serializerOptions));
            tasks.Add(SendEvent(producer, "master_ah", messageKey, CreateMasterAHMessage(messageKey, ahNode, index), serializerOptions));
            tasks.Add(SendEvent(producer, "product_general", messageKey, CreateProductGeneralMessage(messageKey, article, index), serializerOptions));
            tasks.Add(SendEvent(producer, "price", messageKey, CreateProductPriceMessage(messageKey, article, validFrom, validTo), serializerOptions));
        }

        // Wait for all messages to be sent
        await Task.WhenAll(tasks);
        Console.WriteLine("Completed!");
    }

    private static async Task SendEvent<T>(IProducer<string, string> producer, string topic, string key, T message, JsonSerializerOptions options)
    {
        await producer.ProduceAsync(topic, new Message<string, string>
        {
            Key = key,
            Value = JsonSerializer.Serialize(message, options)
        });
    }

    private static BaseEvent<ProductAHMessage> CreateProductAHMessage(string key, string article, string ahNode)
    {
        return new BaseEvent<ProductAHMessage>
        {
            SyncName = "product_ah",
            DocumentNumber = key,
            SyncData = new ProductAHMessage
            {
                ArticleCode = article,
                AhNode = ahNode,
                ValidFrom = "20240705",
                ValidTo = "99991231",
                Main = ""
            }
        };
    }

    private static BaseEvent<ProductListingMessage> CreateProductListingMessage(string key, string article, string site)
    {
        return new BaseEvent<ProductListingMessage>
        {
            SyncName = "product_listing",
            DocumentNumber = key,
            SyncData = new ProductListingMessage
            {
                Article = article,
                ValidFrom = "05.07.2024",
                ValidTo = "31.12.9999",
                Site = site,
                Status = ""
            }
        };
    }

    private static BaseEvent<MasterAHMessage> CreateMasterAHMessage(string key, string ahNode, int index)
    {
        return new BaseEvent<MasterAHMessage>
        {
            SyncName = "master_ah",
            DocumentNumber = key,
            SyncData = new MasterAHMessage
            {
                DeptId = ahNode.Substring(0, 4),
                DeptName = $"Gogocurry_{index}",
                CommId = ahNode.Substring(0, 8),
                CommName = $"Raw_Material_{index}",
                MerchId = ahNode.Substring(0, 10),
                MerchName = $"Meat_{index}",
                ProdgrpId = ahNode,
                ProdgrpName = $"Beef_{index}",
            }
        };
    }

    private static BaseEvent<ProductGeneralMessage> CreateProductGeneralMessage(string key, string article, int index)
    {
        return new BaseEvent<ProductGeneralMessage>
        {
            SyncName = "product_general",
            DocumentNumber = key,
            SyncData = new ProductGeneralMessage
            {
                ArticleCode = article,
                ArticleDesc = $"SAUSAGE_COCKTAIL_{index}",
                BrandCode = "NO BRAND",
                BrandDesc = "NO BRAND",
                BaseUom = "EA",
                ArticleCategory = "12",
                ArticleType = "ZMER",
                OldArticleCode = article,
                OldArticleNumber = article,
                BasicText = $"SAUSAGE_COCKTAIL_{index}",
                DimUom = "CM",
                GrossWeight = "0",
                NetWeight = "0",
                WeightUom = "KG",
                TaxClass = "1",
                ServcAggr = "",
                ServcAggrDesc = "",
                Height = "0",
                Width = "0",
                Length = "0",
                RemShelflife = "0",
                TotShelflife = "0"
            }
        };
    }

    private static BaseEvent<ProductPriceMessage> CreateProductPriceMessage(string key, string article, string validFrom, string validTo)
    {
        return new BaseEvent<ProductPriceMessage>
        {
            SyncName = "price",
            DocumentNumber = key,
            SyncData = new ProductPriceMessage
            {
                PriceA955 = new List<ProductPriceMessageBody>
                {
                    new ProductPriceMessageBody
                    {
                        Application = "V",
                        CondType = "ZRPR",
                        SalesOrg = "F100",
                        DistChannel = 20,
                        PriceList = "Z1",
                        Article = article,
                        ValidFrom = validFrom,
                        ValidTo = validTo,
                        CondRecNo = article,
                        Value = 3000,
                        CreatedOn = validFrom,
                        DelFlag = ""
                    }
                }
            }
        };
    }

    private static string GenerateCharacters(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

}
