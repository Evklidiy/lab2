using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoPartsApp.Models
{
    public class AutoPart
    {
        [Key]
        public int PartId { get; set; }
        public string Article { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Manufacturer { get; set; }
        public string? Compatibility { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }

        public List<SupplyPosition> SupplyPositions { get; set; } = new();
        public List<SalePosition> SalePositions { get; set; } = new();
        public List<Stock> Stocks { get; set; } = new();
    }
}