using System;
using System.ComponentModel.DataAnnotations;
// This is the model for one row in the database table.
namespace BlazorPurchaseOrders.Data
{
    public class POHeader
    {
        [Required]
        public int POHeaderID { get; set; }

        [Required]
        public int POHeaderOrderNumber { get; set; }

        [Required]
        public DateTime POHeaderOrderDate { get; set; }

        [Required]
        public int POHeaderSupplierID { get; set; }

        [StringLength(50, ErrorMessage = "'Address' has a maximum length of 50 characters.")]
        public string POHeaderSupplierAddress1 { get; set; }

        [StringLength(50, ErrorMessage = "'Address' has a maximum length of 50 characters.")]
        public string POHeaderSupplierAddress2 { get; set; }

        [StringLength(50, ErrorMessage = "'Address' has a maximum length of 50 characters.")]
        public string POHeaderSupplierAddress3 { get; set; }

        [StringLength(10, ErrorMessage = "'Post Code' has a maximum length of 10 characters.")]
        public string POHeaderSupplierPostCode { get; set; }

        [StringLength(256, ErrorMessage = "'Email' has a maximum length of 256 characters.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address format.")]
        public string POHeaderSupplierEmail { get; set; }

        [StringLength(450)]
        public string POHeaderRequestedBy { get; set; }

        [Required]
        public bool POHeaderIsArchived { get; set; }

        public string SupplierName { get; }

        public decimal TotalOrderValue { get; }

    }
}
