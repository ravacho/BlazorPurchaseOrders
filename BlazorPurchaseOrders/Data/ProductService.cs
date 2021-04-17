// This is the service for the Product class.
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BlazorPurchaseOrders.Data
{
    public class ProductService : IProductService
    {
        // Database connection
        private readonly SqlConnectionConfiguration _configuration;
        public ProductService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        // Add (create) a Product table row (SQL Insert)
        // This only works if you're already created the stored procedure.
        public async Task<int> ProductInsert(
            string ProductCode,
            string ProductDescription,
            decimal ProductUnitPrice,
            Int32 ProductSupplierID
            )
        {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("ProductCode", ProductCode, DbType.String);
            parameters.Add("ProductDescription", ProductDescription, DbType.String);
            parameters.Add("ProductUnitPrice", ProductUnitPrice, DbType.Decimal);
            parameters.Add("ProductSupplierID", ProductSupplierID, DbType.Int32);
            parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                // Stored procedure method
                await conn.ExecuteAsync("spProduct_Insert", parameters, commandType: CommandType.StoredProcedure);

                Success = parameters.Get<int>("@ReturnValue");
            }
            return Success;
        }

        // Get a list of product rows (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<IEnumerable<Product>> ProductList()
        {
            IEnumerable<Product> products;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                products = await conn.QueryAsync<Product>("spProduct_List", commandType: CommandType.StoredProcedure);
            }
            return products;
        }
        // Get one product based on its ProductID (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<Product> Product_GetOne(int @ProductID)
        {
            Product product = new Product();
            var parameters = new DynamicParameters();
            parameters.Add("@ProductID", ProductID, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                product = await conn.QueryFirstOrDefaultAsync<Product>("spProduct_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            return product;
        }
        // Update one Product row based on its ProductID (SQL Update)
        // This only works if you're already created the stored procedure.
        public async Task<int> ProductUpdate(
                    int ProductID,
                    string ProductCode,
                    string ProductDescription,
                    decimal ProductUnitPrice,
                    Int32 ProductSupplierID,
                    bool ProductIsArchived
                    )
        {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("ProductID", ProductID, DbType.Int32);
            parameters.Add("ProductCode", ProductCode, DbType.String);
            parameters.Add("ProductDescription", ProductDescription, DbType.String);
            parameters.Add("ProductUnitPrice", ProductUnitPrice, DbType.Decimal);
            parameters.Add("ProductSupplierID", ProductSupplierID, DbType.Int32);
            parameters.Add("ProductIsArchived", ProductIsArchived, DbType.Boolean);
            parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                await conn.ExecuteAsync("spProduct_Update", parameters, commandType: CommandType.StoredProcedure);

                Success = parameters.Get<int>
                ("@ReturnValue");
            }
            return Success;
        }

        // Get list of products based on their SupplierID (SQL Select)
        public async Task<IEnumerable<Product>> ProductListBySupplier(int @SupplierID)
        {
            IEnumerable<Product> products;
            var parameters = new DynamicParameters();
            parameters.Add("@SupplierID", SupplierID, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                products = await conn.QueryAsync<Product>("spProduct_ListBySupplier", parameters, commandType: CommandType.StoredProcedure);
            }
            return products;
        }
    }
}

