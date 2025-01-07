using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using MigratorABC.Dto;
using MigratorABC.Entities;
using MigratorProduct.Dto;
using MigratorProduct.Entities;
using MigratorProduct.Entities.Enums;
using Nest;
using System.Text.Json;

namespace MigratorProduct
{
    public class DataImport
    {
        private readonly AppDbContext _appDbContext;

        public DataImport(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task BulkInsertProduct(List<Dto.Product> products)
        {
            Console.WriteLine(products.Count);

            var serializeOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            };

            using var transaction = await _appDbContext.Database.BeginTransactionAsync();
            try
            {
                // Prepare lists for bulk insertion
                var productEntities = new List<Entities.Product>();
                var categoryEntities = new List<ProductCategory>();
                var combinationEntities = new List<ProductCombination>();
                var optionEntities = new List<ProductOption>();

                foreach (var item in products)
                {
                    var productID = Guid.NewGuid();

                    if(item.site_listing != null)
                    {

                    }

                    // Prepare Product entity
                    var product = new Entities.Product
                    {
                        Id = productID,
                        Name = item.name ?? "",
                        DisplayName = item.display_name ?? "",
                        Description = item.description ?? "",
                        ImageUrl = item.image_url ?? "",
                        MetadataCategory = item.metadata.Category != null ? JsonDocument.Parse(JsonSerializer.Serialize(item.metadata.Category)) : JsonDocument.Parse(JsonSerializer.Serialize(new Dto.CodeName())),
                        MetadataCode = item.metadata.Code ?? "",
                        MetadataExternal = item.metadata.External,
                        MetadataDelivery = item.metadata.Delivery,
                        MetadataName = item.name,
                        MetadataPickup = item.metadata.Pickup,
                        Brand = item.brand ?? "",
                        AvailablePlatform = item.available_platform ?? new List<string>(),
                        MetadataSelling = item.metadata.Selling,
                        MetadataSeries = item.metadata.Series != null ? JsonDocument.Parse(JsonSerializer.Serialize(item.metadata.Series)) : JsonDocument.Parse(JsonSerializer.Serialize(new Dto.CodeName())),
                        SiteListing = item.site_listing,
                        UpdatedBy = item.updated_by ?? "system-consumer",
                        UpdatedAt = item.updated_at ?? DateTime.Now,
                        CreatedAt = item.CreatedAt ?? DateTime.Now,
                        CreatedBy = item.CreatedBy ?? "system-consumer",
                        ActiveAt = item.active_at,
                        Characteristic = JsonSerializer.SerializeToDocument(new List<string>()),
                        Type = GetProductType(item),
                        Platform = item.platform != null ?  JsonDocument.Parse(JsonSerializer.Serialize(item.platform, serializeOptions)) : JsonDocument.Parse("{}"),
                        MerchandiseCategoryId = item.merchandise_category_id,
                        TermsAndCondition = item.terms_and_condition,
                        Draft = item.Draft,
                        LastActiveAt = null,
                    };

                    productEntities.Add(product);

                    // Prepare Category entities
                    if (item.categories.Any())
                    {
                        categoryEntities.AddRange(item.categories.Select(categoryItem => new ProductCategory
                        {
                            Id = Guid.NewGuid(),
                            ProductId = productID,
                            Code = categoryItem.code,
                            DefaultCode = categoryItem.default_code,
                            Name = categoryItem.name,
                            QuantityEnabled = categoryItem.quantity_enabled,
                            Required = categoryItem.required,
                            SoldOutRequired = categoryItem.sold_out_required ?? false,
                            Type = GetCategoryType(categoryItem.type),
                            Lang = categoryItem.lang != null ? JsonDocument.Parse(JsonSerializer.Serialize(categoryItem.lang)) : JsonDocument.Parse(JsonSerializer.Serialize(new Lang())),
                            Constrains = categoryItem.constraints != null ? JsonDocument.Parse(JsonSerializer.Serialize(categoryItem.constraints)) : JsonDocument.Parse(JsonSerializer.Serialize(new Constraints())),
                            UpdatedBy = item.updated_by ?? "system-consumer",
                            UpdatedAt = item.updated_at ?? DateTime.Now,
                            CreatedAt = item.CreatedAt ?? DateTime.Now,
                            CreatedBy = item.CreatedBy ?? "system-consumer",
                        }));
                    }

                    // Prepare Combination and Option entities
                    if (item.options.Any() && item.combination.Any())
                    {
                        combinationEntities.AddRange(item.combination.Select(combinationItem => new ProductCombination
                        {
                            Id = Guid.NewGuid(),
                            ProductId = productID,
                            ResultCode = combinationItem.result_code ?? "",
                            Characteristic = JsonSerializer.SerializeToDocument(new List<string>()),
                            CombinationCode = combinationItem.combination_code ?? new List<string>(),
                            RequiredOptionCodes = combinationItem.required_option_codes ?? new List<string>(),
                            UpdatedBy = item.updated_by ?? "system-consumer",
                            UpdatedAt = item.updated_at ?? DateTime.Now,
                            CreatedAt = item.CreatedAt ?? DateTime.Now,
                            CreatedBy = item.CreatedBy ?? "system-consumer",
                        }));

                        optionEntities.AddRange(item.options.Select(optionItem => new ProductOption
                        {
                            Id = Guid.NewGuid(),
                            ProductId = productID,
                            AvailableOn = optionItem.available_on ?? new List<string>(),
                            Code = optionItem.code ?? "",
                            Description = optionItem.description ?? "",
                            Name = optionItem.name ?? "",
                            Category = optionItem.category ?? "",
                            Product = optionItem.Product != null ? JsonDocument.Parse(JsonSerializer.Serialize(optionItem.Product)) : JsonDocument.Parse(JsonSerializer.Serialize(new Dto.CodeName())),
                            UpdatedBy = item.updated_by ?? "system-consumer",
                            UpdatedAt = item.updated_at ?? DateTime.Now,
                            CreatedAt = item.CreatedAt ?? DateTime.Now,
                            CreatedBy = item.CreatedBy ?? "system-consumer",
                        }));
                    }
                }

                Console.WriteLine("Product Length: {0}", productEntities.Count());

                // Perform bulk insert
                await _appDbContext.BulkInsertAsync(productEntities, new BulkConfig() { BatchSize = 5000 });
                await _appDbContext.BulkInsertAsync(categoryEntities, new BulkConfig() { BatchSize = 5000 });
                await _appDbContext.BulkInsertAsync(combinationEntities, new BulkConfig() { BatchSize = 5000 });
                await _appDbContext.BulkInsertAsync(optionEntities, new BulkConfig() { BatchSize = 5000 });

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new ArgumentException($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task BulkInsertOptionGroup(List<Dto.OptionGroup> optionGroups)
        {
            var optionGroupList = new List<Entities.OptionGroup>();

            foreach (var optionGroup in optionGroups)
            {
                optionGroupList.Add(new Entities.OptionGroup()
                {
                    Id = optionGroup.ID,
                    Name = optionGroup.Name,
                    Type = optionGroup.Type,
                    SI001 = optionGroup.SI001,
                    SI002 = optionGroup.SI002,
                    SI003 = optionGroup.SI003,
                    SI007 = optionGroup.SI007,
                    CreatedAt = optionGroup.CreatedAt,
                    CreatedBy = optionGroup.CreatedBy,
                    UpdatedAt = optionGroup.UpdatedAt,
                    UpdatedBy = optionGroup.UpdatedBy,
                });
            }

            await _appDbContext.OptionGroups.AddRangeAsync(optionGroupList);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task BulkInsertProductPrice(List<ProductPriceDto> prices)
        {
            Console.WriteLine("total data: {0}", prices.Count());

            var newPrices = new List<ProductPrice>();

            foreach (var item in prices)
            {
                if (string.IsNullOrEmpty(item.product_code) || string.IsNullOrEmpty(item.site_code))
                    continue;

                newPrices.Add(new ProductPrice()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = item.created_by,
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = item.updated_by,
                    SapUpdatedAt = DateTime.UtcNow,
                    // Handle serialization error and set Grab to null if an error occurs
                    Price = item.Price,
                    PriceWithoutTax = ConvertToIntOrZero(item.PriceWithoutTax),
                    ProductCode = item.product_code,
                    SiteCode = item.site_code,
                    SoldOut = item.SoldOut,
                    Tax = ConvertToIntOrZero(item.Tax),
                    Active = item.Active,
                    DistChannel = 20,
                    
                });

                if(item.Grab != null && item.Grab?.Price != 0)
                {
                    newPrices.Add(new ProductPrice()
                    {
                       Id = Guid.NewGuid(),
                       Price = item.Grab.Price, 
                       PriceWithoutTax = item.Grab.PriceWithoutTax ?? 0,
                       Active = item.Grab.Active,
                       ProductCode = item.product_code,
                       SiteCode = item.site_code,
                       SoldOut = item.Grab.SoldOut,
                       Tax = item.Grab.Tax ?? 0,
                       CreatedAt = DateTime.UtcNow,
                       CreatedBy = item.created_by,
                       UpdatedAt = DateTime.UtcNow,
                       UpdatedBy = item.updated_by,
                       SapUpdatedAt = item.PimUpdatedAt ?? null,
                       DistChannel = 80,
                    });
                }
            }

            await _appDbContext.BulkInsertAsync(newPrices);
            await _appDbContext.SaveChangesAsync();
        }

        private int ConvertToIntOrZero(string value)
        {
            // Try parsing the string to decimal and convert it to int
            if (decimal.TryParse(value, out decimal parsedValue))
            {
                // Round the decimal value and convert to int
                return (int)Math.Round(parsedValue); // Use Math.Floor or Math.Ceiling if needed
            }

            // Return 0 if parsing fails or if the value is empty
            return 0;
        }

        private JsonDocument TrySerialize(object grab)
        {
            try
            {
                return JsonSerializer.SerializeToDocument(grab);
            }
            catch (Exception)
            {
                // Return null if serialization fails
                return null;
            }
        }


        private ProductType GetProductType(Dto.Product product)
        {
            var allCombinationsHaveZeroCodeCount = product.combination.All(x => x.combination_code.Count() == 0);

            if (product.combination.Count <= 1 && allCombinationsHaveZeroCodeCount)
                return ProductType.Single;
            else
                return ProductType.Group;

        }

        private ProductCategoryType GetCategoryType(string category)
        {
            switch (category)
            {
                case "checkbox":
                    return ProductCategoryType.Checkbox;
                case "radio":
                    return ProductCategoryType.Radio;
                case "auto_apply":
                    return ProductCategoryType.AutoApply;
                default:
                    return ProductCategoryType.Checkbox;
            }
        }


    }
}
