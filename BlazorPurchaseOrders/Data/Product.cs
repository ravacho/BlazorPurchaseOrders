using System;
using System.ComponentModel.DataAnnotations;
// This is the model for one row in the database table. You may need to make some adjustments.
namespace BlazorPurchaseOrders.Data
{
    public class Product
    {
        [Required]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "'Product Code' is required.")]
        [StringLength(25, ErrorMessage = "'Product Code' has a maximum length of 25 characters.")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "'Description' is required.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "'Description' has a minimum length of 5 and a maximum of 25 characters.")]
        public string ProductDescription { get; set; }

        [Required]
        public decimal ProductUnitPrice { get; set; }

        public int ProductSupplierID { get; set; }

        [Required]
        public bool ProductIsArchived { get; set; }

        public string SupplierName { get; }
    }
}