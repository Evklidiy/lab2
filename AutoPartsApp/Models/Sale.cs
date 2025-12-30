using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoPartsApp.Models
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }
        public string? ClientName { get; set; }
        public string? CarModel { get; set; }
        public DateTime SaleDate { get; set; }

        public List<SalePosition> Positions { get; set; } = new();
    }
}