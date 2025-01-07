using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MigratorProduct.Entities.Enums;
using Npgsql;
using Npgsql.NameTranslation;

namespace MigratorProduct.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<ProductType>("product_type", new NpgsqlNullNameTranslator());
            NpgsqlConnection.GlobalTypeMapper.MapEnum<ProductCategoryType>("product_category_type", new NpgsqlNullNameTranslator());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<ProductCombination> ProductCombinations { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<OptionGroup> OptionGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("Host=localhost;Database=product_dev;Username=postgres;Password=P@ssw0rd#@!");
        }
            
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<ProductType>(name: "product_type");
            modelBuilder.HasPostgresEnum<ProductCategoryType>(name: "product_category_type");

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Type)
                      .HasColumnType("product_type");
                entity.Property(pc => pc.Characteristic)
                    .HasColumnType("jsonb");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(pc => pc.Id);
                entity.Property(p => p.Type)
                      .HasColumnType("product_category_type");

                entity.Property(pc => pc.Lang)
                      .HasColumnType("jsonb");
                entity.Property(pc => pc.Constrains)
                      .HasColumnType("jsonb");
            });

            modelBuilder.Entity<ProductOption>(entity =>
            {
                entity.HasKey(po => po.Id);
                entity.Property(po => po.Product)
                      .HasColumnType("jsonb");
            });

            modelBuilder.Entity<ProductCombination>(entity =>
            {
                entity.HasKey(po => po.Id);
                entity.Property(po => po.Characteristic)
                      .HasColumnType("jsonb");
            });

            modelBuilder.Entity<ProductPrice>(entity =>
            {
                entity.HasKey(po => po.Id);
            });

            modelBuilder.Entity<OptionGroup>()
                .ToTable("option_group")
                .HasKey(og => og.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
