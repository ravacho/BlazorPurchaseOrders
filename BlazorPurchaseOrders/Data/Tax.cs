using System;
using System.ComponentModel.DataAnnotations;
// This is the model for one row in the database table. You may need to make some adjustments.
namespace BlazorPurchaseOrders.Data
{
    public class Tax
    {
        [Required]
        public int TaxID { get; set; }
        [Required]
        [StringLength(50)]
        public string TaxDescription { get; set; }
        [Required]
        public decimal TaxRate { get; set; }
        [Required]
        public bool TaxIsArchived { get; set; }

    }
}