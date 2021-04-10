using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BlazorPurchaseOrders.Data
{
    public class TaxService : ITaxService
    {
        // Database connection
        private readonly SqlConnectionConfiguration _configuration;
        public TaxService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        // Add (create) a Tax table row (SQL Insert)
        // This only works if you're already created the stored procedure.
        public async Task<int> TaxInsert(string TaxDescription, Decimal TaxRate)
        {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("TaxDescription", TaxDescription, DbType.String);
            parameters.Add("TaxRate", TaxRate, DbType.Decimal);
            parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                // Stored procedure method
                await conn.ExecuteAsync("spTax_Insert", parameters, commandType: CommandType.StoredProcedure);

                Success = parameters.Get<int>("@ReturnValue");
            }
            return Success;
        }
        // Get a list of tax rows (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<IEnumerable<Tax>> TaxList()
        {
            IEnumerable<Tax> taxes;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                taxes = await conn.QueryAsync<Tax>("spTax_List", commandType: CommandType.StoredProcedure);
            }
            return taxes;
        }
        // Get one tax based on its TaxID (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<Tax> Tax_GetOne(int @TaxID)
        {
            Tax tax = new Tax();
            var parameters = new DynamicParameters();
            parameters.Add("@TaxID", TaxID, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                tax = await conn.QueryFirstOrDefaultAsync<Tax>("spTax_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            return tax;
        }
        // Update one Tax row based on its TaxID (SQL Update)
        // This only works if you're already created the stored procedure.
        public async Task<int> TaxUpdate(string TaxDescription, decimal TaxRate, int TaxID, bool TaxIsArchived)
        {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("TaxDescription", TaxDescription, DbType.String);
            parameters.Add("TaxRate", TaxRate, DbType.Decimal);
            parameters.Add("TaxId", TaxID, DbType.Int32);
            parameters.Add("TaxIsArchived", TaxIsArchived, DbType.Boolean);
            parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                await conn.ExecuteAsync("spTax_Update", parameters, commandType: CommandType.StoredProcedure);

                Success = parameters.Get<int>
                ("@ReturnValue");
            }
            return Success;
        }
    }
}