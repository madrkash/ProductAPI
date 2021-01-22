using Dapper;
using Npgsql;
using ProductStore.Core.Contracts;
using ProductStore.Core.Models;
using ProductStore.Infrastructure.Configs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductStore.Infrastructure.Data
{
    public class ProductOptionRepository : RepositoryBase<ProductOption>, IProductOptionRepository
    {
        private const string WhereClause = "where ProductId = @Id";

        public ProductOptionRepository(DatabaseConfig configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<ProductOption>> GetAllOptionsByProductIdAsync(Guid id)
        {
            await using var conn = new NpgsqlConnection(ConnectionString);
            var queryResult = await conn.GetListAsync<ProductOption>(WhereClause, new { Id = id });
            return queryResult;
        }

        public async Task<int> DeleteListAsync(Guid productId)
        {
            await using var conn = new NpgsqlConnection(ConnectionString);
            var queryResult = await conn.DeleteListAsync<ProductOption>(WhereClause, new { Id = productId });
            return queryResult;
        }
    }
}