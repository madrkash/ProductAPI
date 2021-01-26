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
    public abstract class RepositoryBase<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly string ConnectionString;

        protected RepositoryBase(DatabaseConfig configuration)
        {
            ConnectionString = configuration.ConnectionString;
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            await using var conn = new NpgsqlConnection(ConnectionString);
            var queryResult = await conn.GetAsync<T>(id);
            return queryResult;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            await using var conn = new NpgsqlConnection(ConnectionString);
            var queryResult = await conn.GetListAsync<T>();
            return queryResult;
        }

        public async Task<Guid> AddAsync(T entity)
        {
            await using var conn = new NpgsqlConnection(ConnectionString);
            var queryResult = await conn.InsertAsync<Guid, T>(entity);
            return queryResult;
        }

        public async Task<int> UpdateAsync(T entity)
        {
            await using var conn = new NpgsqlConnection(ConnectionString);
            var queryResult = await conn.UpdateAsync<T>(entity);
            return queryResult;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            await using var conn = new NpgsqlConnection(ConnectionString);
            var queryResult = await conn.DeleteAsync<T>(id);
            return queryResult;
        }
    }
}