using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoPartsApp.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        public string Name { get; set; } = null!;
        public string? ContactInfo { get; set; }
        public double? Rating { get; set; }
        public List<Supply> Supplies { get; set; } = new();
    }
}