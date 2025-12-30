using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoPartsApp.Models
{
    public class Stock
    {
        [Key]
        public int StockId { get; set; }
        public int PartId { get; set; }
        public int Quantity { get; set; }
        public string? LocationShelf { get; set; }

        [ForeignKey("PartId")]
        public AutoPart AutoPart { get; set; } = null!;
    }
}