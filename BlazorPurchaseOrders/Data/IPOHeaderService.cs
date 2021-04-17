using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorPurchaseOrders.Data
{
    // Each item below provides an interface to a method in POHeaderServices.cs
    public interface IPOHeaderService
    {
        Task<int> POHeaderInsert(
            DateTime POHeaderOrderDate,
            int POHeaderSupplierID,
            string POHeaderSupplierAddress1,
            string POHeaderSupplierAddress2,
            string POHeaderSupplierAddress3,
            string POHeaderSupplierPostCode,
            string POHeaderSupplierEmail,
            string POHeaderRequestedBy
            );
        Task<IEnumerable<POHeader>> POHeaderList();
        Task<POHeader> POHeader_GetOne(int POHeaderID);
        Task<bool> POHeaderUpdate(POHeader poheader);
    }
}