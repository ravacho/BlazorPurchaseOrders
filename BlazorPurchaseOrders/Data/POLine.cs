using System;
using System.ComponentModel.DataAnnotations;

// This is the model for one row in the database table.
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
        [Required(ErrorMessage = "Product Description is compulsory.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Product Description must be between 2 and 20 characters.")]
        public string POLineProductDescription { get; set; }
        [Required]
        public decimal POLineProductQuantity { get; set; }
        [Required]
        public decimal POLineProductUnitPrice { get; set; }
        [Required]
        public decimal POLineTaxRate { get; set; }
        public int POLineTaxID { get; set; }

        //The following are not saved to database - just for the DataGrid
        public decimal? POLineNetPrice { get; set; }
        public decimal POLineTaxAmount { get; set; }
        public decimal POLineGrossPrice { get; set; }
        public string POLineProductCode { get; set; }

    }
}