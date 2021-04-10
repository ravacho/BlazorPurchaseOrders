// This is the service for the Supplier class.
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BlazorPurchaseOrders.Data
{
    public class SupplierService : ISupplierService
    {
        // Database connection
        private readonly SqlConnectionConfiguration _configuration;
        public SupplierService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        // Add (create) a Supplier table row (SQL Insert)
        // This only works if you're already created the stored procedure.
        public async Task<int> SupplierInsert(string SupplierName, string SupplierAddress1,
            string SupplierAddress2, string SupplierAddress3, string SupplierPostCode,
            string SupplierEmail)
        {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("SupplierName", SupplierName, DbType.String);
            parameters.Add("SupplierAddress1", SupplierAddress1, DbType.String);
            parameters.Add("SupplierAddress2", SupplierAddress2, DbType.String);
            parameters.Add("SupplierAddress3", SupplierAddress3, DbType.String);
            parameters.Add("SupplierPostCode", SupplierPostCode, DbType.String);
            parameters.Add("SupplierEmail", SupplierEmail, DbType.String);
            parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                // Stored procedure method
                await conn.ExecuteAsync("spSupplier_Insert", parameters, commandType: CommandType.StoredProcedure);

                Success = parameters.Get<int>("@ReturnValue");
            }
            return Success;
        }
        // Get a list of supplier rows (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<IEnumerable<Supplier>> SupplierList()
        {
            IEnumerable<Supplier> suppliers;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                suppliers = await conn.QueryAsync<Supplier>("spSupplier_List", commandType: CommandType.StoredProcedure);
            }
            return suppliers;
        }

        // Get one supplier based on its SupplierID (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<Supplier> Supplier_GetOne(int @SupplierID)
        {
            Supplier supplier = new Supplier();
            var parameters = new DynamicParameters();
            parameters.Add("@SupplierID", SupplierID, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                supplier = await conn.QueryFirstOrDefaultAsync<Supplier>("spSupplier_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            return supplier;
        }
        // Update one Supplier row based on its SupplierID (SQL Update)
        // This only works if you're already created the stored procedure.
        public async Task<int> SupplierUpdate(int SupplierID, string SupplierName, string SupplierAddress1,
        string SupplierAddress2, string SupplierAddress3, string SupplierPostCode,
        string SupplierEmail, bool SupplierIsArchived)
        {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("SupplierID", SupplierID, DbType.Int32);
            parameters.Add("SupplierName", SupplierName, DbType.String);
            parameters.Add("SupplierAddress1", SupplierAddress1, DbType.String);
            parameters.Add("SupplierAddress2", SupplierAddress2, DbType.String);
            parameters.Add("SupplierAddress3", SupplierAddress3, DbType.String);
            parameters.Add("SupplierPostCode", SupplierPostCode, DbType.String);
            parameters.Add("SupplierEmail", SupplierEmail, DbType.String);
            parameters.Add("SupplierIsArchived", SupplierIsArchived, DbType.Boolean);
            parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                await conn.ExecuteAsync("spSupplier_Update", parameters, commandType: CommandType.StoredProcedure);

                Success = parameters.Get<int>
                ("@ReturnValue");
            }
            return Success;
        }
    }

    
}

