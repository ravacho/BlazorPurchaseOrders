using System;
using System.ComponentModel.DataAnnotations;
// This is the model for one row in the database table. You may need to make some adjustments.
namespace BlazorPurchaseOrders.Data
{
    public class POLine
    {
        [Required]
        public int POLineID { get; set; }
        [Required]
        public int POLineHeaderID { get; set; }
        [Required]
        public int POLineProductID { get; set; }
        [Required]
        [StringLength(50)]
        public string POLineProductDescription { get; set; }
        [Required]
        public decimal POLineProductQuantity { get; set; }
        [Required]
        public decimal POLineProductUnitPrice { get; set; }
        [Required]
        public decimal POLineTaxRate { get; set; }

    }
}