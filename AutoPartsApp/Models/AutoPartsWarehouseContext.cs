using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AutoPartsApp.Models
{
    public class AutoPartsWarehouseContext : DbContext
    {
        public DbSet<AutoPart> AutoParts { get; set; } = null!;
        public DbSet<Supplier> Suppliers { get; set; } = null!;
        public DbSet<Supply> Supplies { get; set; } = null!;
        public DbSet<SupplyPosition> SupplyPositions { get; set; } = null!;
        public DbSet<Sale> Sales { get; set; } = null!;
        public DbSet<SalePosition> SalePositions { get; set; } = null!;
        public DbSet<Stock> Stocks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                var config = builder.Build();
                string connectionString = config.GetConnectionString("DefaultConnection");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}