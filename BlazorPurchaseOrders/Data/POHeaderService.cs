// This is the service for the POHeader class.
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BlazorPurchaseOrders.Data
{
    public class POHeaderService : IPOHeaderService
    {
        // Database connection
        private readonly SqlConnectionConfiguration _configuration;
        public POHeaderService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        // Add (create) a POHeader table row (SQL Insert)
        // This only works if you're already created the stored procedure.
        public async Task<bool> POHeaderInsert(POHeader poheader)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                //parameters.Add("POHeaderOrderNumber", poheader.POHeaderOrderNumber, DbType.Int32);
                parameters.Add("POHeaderOrderDate", poheader.POHeaderOrderDate, DbType.Date);
                parameters.Add("POHeaderSupplierID", poheader.POHeaderSupplierID, DbType.Int32);
                parameters.Add("POHeaderSupplierAddress1", poheader.POHeaderSupplierAddress1, DbType.String);
                parameters.Add("POHeaderSupplierAddress2", poheader.POHeaderSupplierAddress2, DbType.String);
                parameters.Add("POHeaderSupplierAddress3", poheader.POHeaderSupplierAddress3, DbType.String);
                parameters.Add("POHeaderSupplierPostCode", poheader.POHeaderSupplierPostCode, DbType.String);
                parameters.Add("POHeaderSupplierEmail", poheader.POHeaderSupplierEmail, DbType.String);
                parameters.Add("POHeaderRequestedBy", poheader.POHeaderRequestedBy, DbType.String);
                parameters.Add("POHeaderIsArchived", poheader.POHeaderIsArchived, DbType.Boolean);

                // Stored procedure method
                await conn.ExecuteAsync("spPOHeader_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
        // Get a list of poheader rows (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<IEnumerable<POHeader>> POHeaderList()
        {
            IEnumerable<POHeader> poheaders;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                poheaders = await conn.QueryAsync<POHeader>("spPOHeader_List", commandType: CommandType.StoredProcedure);
            }
            return poheaders;
        }
        // Get one poheader based on its POHeaderID (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<POHeader> POHeader_GetOne(int @POHeaderID)
        {
            POHeader poheader = new POHeader();
            var parameters = new DynamicParameters();
            parameters.Add("@POHeaderID", POHeaderID, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                poheader = await conn.QueryFirstOrDefaultAsync<POHeader>("spPOHeader_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            return poheader;
        }
        // Update one POHeader row based on its POHeaderID (SQL Update)
        // This only works if you're already created the stored procedure.
        public async Task<bool> POHeaderUpdate(POHeader poheader)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("POHeaderID", poheader.POHeaderID, DbType.Int32);

                parameters.Add("POHeaderOrderNumber", poheader.POHeaderOrderNumber, DbType.Int32);
                parameters.Add("POHeaderOrderDate", poheader.POHeaderOrderDate, DbType.Date);
                parameters.Add("POHeaderSupplierID", poheader.POHeaderSupplierID, DbType.Int32);
                parameters.Add("POHeaderSupplierAddress1", poheader.POHeaderSupplierAddress1, DbType.String);
                parameters.Add("POHeaderSupplierAddress2", poheader.POHeaderSupplierAddress2, DbType.String);
                parameters.Add("POHeaderSupplierAddress3", poheader.POHeaderSupplierAddress3, DbType.String);
                parameters.Add("POHeaderSupplierPostCode", poheader.POHeaderSupplierPostCode, DbType.String);
                parameters.Add("POHeaderSupplierEmail", poheader.POHeaderSupplierEmail, DbType.String);
                parameters.Add("POHeaderRequestedBy", poheader.POHeaderRequestedBy, DbType.String);
                parameters.Add("POHeaderIsArchived", poheader.POHeaderIsArchived, DbType.Boolean);

                await conn.ExecuteAsync("spPOHeader_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
    }
}
