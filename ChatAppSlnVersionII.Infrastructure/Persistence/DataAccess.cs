using ChatAppSlnVersionII.Domain.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Infrastructure.Persistence
{
    public class DataAccess : IDataAccess
    {
        private readonly IDbConnection _dbConnection;
        public DataAccess(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        private IDbConnection CreateConnection()
        {
            return _dbConnection;
        }

        public async Task<int> ExecuteAsync(string sql, DynamicParameters parameters = null, bool isProcedure = false)
        {
            using var connection = CreateConnection();
            connection.Open();
            return await connection.ExecuteAsync(sql, parameters, commandType: isProcedure ? CommandType.StoredProcedure : CommandType.Text);
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, DynamicParameters parameters = null, bool isProcedure = false)
        {
            using var connection = CreateConnection();
            connection.Open();
            return await connection.ExecuteScalarAsync<T>(sql, parameters, commandType: isProcedure ? CommandType.StoredProcedure : CommandType.Text);
        }

        public async Task<List<T>> ExecuteListAsync<T>(string sql, DynamicParameters parameters = null, bool isProcedure = false)
        {
            using var connection = CreateConnection();
            connection.Open();
            var result = await connection.QueryAsync<T>(sql, parameters, commandType: isProcedure ? CommandType.StoredProcedure : CommandType.Text);
            return result.ToList();
        }

        public async Task<(List<T1>, List<T2>)> ExecuteHeaderDetailsAsync<T1, T2>(string sql, DynamicParameters parameters = null, bool isProcedure = false)
        {
            using var connection = CreateConnection();
            connection.Open();
            using var multi = await connection.QueryMultipleAsync(sql, parameters, commandType: isProcedure ? CommandType.StoredProcedure : CommandType.Text);
            var list1 = (await multi.ReadAsync<T1>()).ToList();
            var list2 = (await multi.ReadAsync<T2>()).ToList();
            return (list1, list2);
        }

        public async Task<(List<T1>, List<T2>, List<T3>)> ExecuteHeaderDetailsInfoAsync<T1, T2, T3>(string sql, DynamicParameters parameters = null, bool isProcedure = false)
        {
            using var connection = CreateConnection();
            connection.Open();
            using var multi = await connection.QueryMultipleAsync(sql, parameters, commandType: isProcedure ? CommandType.StoredProcedure : CommandType.Text);
            var list1 = (await multi.ReadAsync<T1>()).ToList();
            var list2 = (await multi.ReadAsync<T2>()).ToList();
            var list3 = (await multi.ReadAsync<T3>()).ToList();
            return (list1, list2, list3);
        }
    }
}
