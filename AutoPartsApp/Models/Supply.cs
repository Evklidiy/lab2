using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoPartsApp.Models
{
    public class Supply
    {
        [Key]
        public int SupplyId { get; set; }
        public int SupplierId { get; set; }
        public DateTime SupplyDate { get; set; }
        public string? Status { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; } = null!;
        public List<SupplyPosition> Positions { get; set; } = new();
    }
}