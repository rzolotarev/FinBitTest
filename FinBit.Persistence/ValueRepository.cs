using Dapper;
using FinBit.Services.Contracts;
using FinBit.Services.Contracts.Models;
using FinBitTest.Services.Contracts.Dtos;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Transactions;

namespace FinBit.Persistence
{
    public class ValueRepository : IValueRepository
    {
        private readonly string _connectionString;
        public ValueRepository(IOptions<DbConnectionString> connection)
        {
            _connectionString = connection?.Value.ConnectionString ?? throw new ArgumentNullException(nameof(connection));
        }

        public async Task<IEnumerable<ValueDto>> GetAsync(ValueFilter filter, Log log)
        {
            var query = BuildCondition(filter, out DynamicParameters parameters);


            using (var db = new SqlConnection(_connectionString))
            {
                await db.ExecuteAsync("INSERT INTO Loggs VALUES (@Query, @Payload)", log);
                var values = await db.QueryAsync<PersistenceValue>($"SELECT Id, Code, Value FROM [Values] {query}", filter);
                return values.Select(v => new ValueDto() { Id = v.Id, Code = v.Code, Value = v.Value }).ToArray();
            }
        }

        public async Task SaveAsync(IEnumerable<PersistenceValue> values, Log log)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    await db.ExecuteAsync("INSERT INTO Loggs VALUES (@Query, @Payload)", log);
                    await db.ExecuteAsync("Delete from [Values]");
                    await db.ExecuteAsync("INSERT INTO [Values] VALUES (@Id, @Code, @Value)", values);
                }
                transactionScope.Complete();
            }
        }

        private string BuildCondition(ValueFilter filter, out DynamicParameters parameters)
        {
            parameters = new DynamicParameters();

            var filterCollection = new List<string>();
            
            if (filter.Code.HasValue)
            {
                filterCollection.Add("Code = @Code");
                parameters.Add("@Code", filter.Code);
            }

            if (!string.IsNullOrWhiteSpace(filter.Value?.Trim()))
            {
                filterCollection.Add("Value = @Value");
                parameters.Add("@Value", filter.Value);
            }

            var conjunction = filter.And ? " AND " : " OR ";
            var filterQuery = string.Join(conjunction, filterCollection);
            return string.IsNullOrWhiteSpace(filterQuery) ? filterQuery : $"WHERE {filterQuery}";
        }
    }
}