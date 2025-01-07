using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MigratorABC.Dto;
using MigratorProduct.Entities;
using Nest;

namespace MigratorProduct
{
    public class DataExport
    {
        private readonly IElasticClient _elasticClient;

        public DataExport(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<List<Dto.Product>> GetAllProductAsync()
        {
            try
            {
                var allProducts = new List<Dto.Product>();
                int batchSize = 10000;
                int from = 0;

                bool hasMore = true;
                var searchResponse = await _elasticClient.SearchAsync<Dto.Product>(s => s
                                       .Index("product")
                                       .Size(batchSize)
                                       .Query(q => q.MatchAll())
                                   );

                Console.WriteLine(searchResponse.DebugInformation);

                if (!searchResponse.IsValid)
                {
                    Console.WriteLine($"Error retrieving data from Elasticsearch: {searchResponse.OriginalException.Message}");
                }

                var products = searchResponse.Documents;
                allProducts.AddRange(products);


                return allProducts;
            }
            catch(Exception ex)
            {
                throw new ArgumentException($"An error ocurred: {ex.Message}");
            }

           
        }


        public async Task<List<Dto.OptionGroup>> GetAllOptionGroupAsync()
        {
            try
            {
                var allProducts = new List<Dto.OptionGroup>();
                int batchSize = 1000;
                int from = 0;
                bool hasMore = true;


                var searchResponse = await _elasticClient.SearchAsync<Dto.OptionGroup>(s => s
               .Index("option_group")
               .From(from)
               .Size(batchSize)
               .Query(q => q.MatchAll())
                );

                Console.WriteLine(searchResponse.DebugInformation);

                if (!searchResponse.IsValid)
                {
                    Console.WriteLine($"Error retrieving data from Elasticsearch: {searchResponse.OriginalException.Message}");
                }

                var products = searchResponse.Documents;
                allProducts.AddRange(products);

                return allProducts;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"An error ocurred: {ex.Message}");
            }


        }

   


    }
}
