using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoPartsApp.Models
{
    public class SupplyPosition
    {
        [Key]
        public int SupplyPositionId { get; set; }
        public int SupplyId { get; set; }
        public int PartId { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }

        [ForeignKey("SupplyId")]
        public Supply Supply { get; set; } = null!;
        [ForeignKey("PartId")]
        public AutoPart AutoPart { get; set; } = null!;
    }
}