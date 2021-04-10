using System;
using System.ComponentModel.DataAnnotations;
// This is the model for one row in the database table. You may need to make some adjustments.
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
        [StringLength(50)]
        public string POHeaderSupplierAddress1 { get; set; }
        [StringLength(50)]
        public string POHeaderSupplierAddress2 { get; set; }
        [StringLength(50)]
        public string POHeaderSupplierAddress3 { get; set; }
        [StringLength(10)]
        public string POHeaderSupplierPostCode { get; set; }
        [StringLength(256)]
        public string POHeaderSupplierEmail { get; set; }
        [StringLength(450)]
        public string POHeaderRequestedBy { get; set; }
        [Required]
        public bool POHeaderIsArchived { get; set; }

    }
}
