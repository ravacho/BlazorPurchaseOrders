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

        //The folowing are not saved to database - just for the DataGrid
        public decimal? POLineNetPrice { get; set; }
        public decimal POLineTaxAmount { get; set; }
        public decimal POLineGrossPrice { get; set; }
        public string POLineProductCode { get; set; }

        //POLIneTaxID is not saved to the database, but is needed for the Tax Rate drop-down list
        //It would be more usual to save the TaxID to POLine in the database, but because
        //tax rate percentages might change in future for a particular 'rate' we don't want
        //historic tax amounts to be recalculated if re-displayed in the future. 
        public int POLineTaxID { get; set; }
    }
}