using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Domain.Interfaces
{
    public interface IDataAccess
    {
        Task<int> ExecuteAsync(string sql, DynamicParameters parameters = null, bool isProcedure = false);
        Task<T> ExecuteScalarAsync<T>(string sql, DynamicParameters parameters = null, bool isProcedure = false);
        Task<List<T>> ExecuteListAsync<T>(string sql, DynamicParameters parameters = null, bool isProcedure = false);
        Task<(List<T1>, List<T2>)> ExecuteHeaderDetailsAsync<T1, T2>(string sql, DynamicParameters parameters = null, bool isProcedure = false);
        Task<(List<T1>, List<T2>, List<T3>)> ExecuteHeaderDetailsInfoAsync<T1, T2, T3>(string sql, DynamicParameters parameters = null, bool isProcedure = false);
    }
}
