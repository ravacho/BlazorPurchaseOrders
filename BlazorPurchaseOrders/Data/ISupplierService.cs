using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorPurchaseOrders.Data
{
    // Each item below provides an interface to a method in SupplierServices.cs
    public interface ISupplierService
    {
        Task<int> SupplierInsert(string SupplierName, string SupplierAddress1,
            string SupplierAddress2, string SupplierAddress3, string SupplierPostCode,
            string SupplierEmail);
        Task<IEnumerable<Supplier>> SupplierList();
        Task<Supplier> Supplier_GetOne(int SupplierID);
        Task<int> SupplierUpdate(int SupplierID, string SupplierName, string SupplierAddress1,
            string SupplierAddress2, string SupplierAddress3, string SupplierPostCode,
            string SupplierEmail, bool SupplierIsArchived);
    }
}