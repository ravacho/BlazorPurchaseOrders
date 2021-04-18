using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorPurchaseOrders.Data
{
    // Each item below provides an interface to a method in POLineServices.cs
    public interface IPOLineService
    {
        Task<bool> POLineInsert(POLine poline);
        Task<IEnumerable<POLine>> POLineList();
        Task<POLine> POLine_GetOne(int POLineID);
        Task<bool> POLineUpdate(POLine poline);
        Task<IEnumerable<POLine>> POLine_GetByPOHeader(int @POHeaderID);
        Task<bool> POLineDeleteOne(int @POLineID);
    }
}