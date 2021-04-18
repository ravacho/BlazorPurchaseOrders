using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BlazorPurchaseOrders.Data
{
    public class POLineService : IPOLineService
    {
        // Database connection
        private readonly SqlConnectionConfiguration _configuration;
        public POLineService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        // Add (create) a POLine table row (SQL Insert)
        // This only works if you're already created the stored procedure.
        public async Task<bool> POLineInsert(POLine poline)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("POLineHeaderID", poline.POLineHeaderID, DbType.Int32);
                parameters.Add("POLineProductID", poline.POLineProductID, DbType.Int32);
                parameters.Add("POLineProductDescription", poline.POLineProductDescription, DbType.String);
                parameters.Add("POLineProductQuantity", poline.POLineProductQuantity, DbType.Decimal);
                parameters.Add("POLineProductUnitPrice", poline.POLineProductUnitPrice, DbType.Decimal);
                parameters.Add("POLineTaxRate", poline.POLineTaxRate, DbType.Decimal);
                parameters.Add("POLineTaxID", poline.POLineTaxID, DbType.Decimal);

                // Stored procedure method
                await conn.ExecuteAsync("spPOLine_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
        // Get a list of poline rows (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<IEnumerable<POLine>> POLineList()
        {
            IEnumerable<POLine> polines;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                polines = await conn.QueryAsync<POLine>("spPOLine_List", commandType: CommandType.StoredProcedure);
            }
            return polines;
        }

        // Get one poline based on its POLineID (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<POLine> POLine_GetOne(int @POLineID)
        {
            POLine poline = new POLine();
            var parameters = new DynamicParameters();
            parameters.Add("@POLineID", POLineID, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                poline = await conn.QueryFirstOrDefaultAsync<POLine>("spPOLine_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            return poline;
        }

        public async Task<IEnumerable<POLine>> POLine_GetByPOHeader(int @POHeaderID)
        {
            IEnumerable<POLine> polines;
            var parameters = new DynamicParameters();
            parameters.Add("@POHeaderID", POHeaderID, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                polines = await conn.QueryAsync<POLine>("spPOLine_GetByPOHeader", parameters, commandType: CommandType.StoredProcedure);
            }
            return polines;
        }

        // Update one POLine row based on its POLineID (SQL Update)
        // This only works if you're already created the stored procedure.
        public async Task<bool> POLineUpdate(POLine poline)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("POLineID", poline.POLineID, DbType.Int32);

                parameters.Add("POLineHeaderID", poline.POLineHeaderID, DbType.Int32);
                parameters.Add("POLineProductID", poline.POLineProductID, DbType.Int32);
                parameters.Add("POLineProductDescription", poline.POLineProductDescription, DbType.String);
                parameters.Add("POLineProductQuantity", poline.POLineProductQuantity, DbType.Decimal);
                parameters.Add("POLineProductUnitPrice", poline.POLineProductUnitPrice, DbType.Decimal);
                parameters.Add("POLineTaxRate", poline.POLineTaxRate, DbType.Decimal);
                parameters.Add("POLineTaxID", poline.POLineTaxID, DbType.Decimal);

                await conn.ExecuteAsync("spPOLine_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }

        public async Task<bool> POLineDeleteOne(int @POLineID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@POLineID", POLineID, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                await conn.ExecuteAsync("spPOLine_DeleteOne", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
    }
}