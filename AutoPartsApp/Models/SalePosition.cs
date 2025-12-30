using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoPartsApp.Models
{
    public class SalePosition
    {
        [Key]
        public int SalePositionId { get; set; }
        public int SaleId { get; set; }
        public int PartId { get; set; }
        public int Quantity { get; set; }
        public decimal SalePrice { get; set; }

        [ForeignKey("SaleId")]
        public Sale Sale { get; set; } = null!;
        [ForeignKey("PartId")]
        public AutoPart AutoPart { get; set; } = null!;
    }
}