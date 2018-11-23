using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using WebClient.Core;
using WebClient.Core.Entities;
using WebClient.Repositories.Interfaces;

namespace WebClient.Repositories.Implements
{
    /// <summary>
    /// Account repository
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        /// <summary>
        /// Login the user
        /// </summary>
        /// <param name="username">The usename</param>
        /// <param name="password">The password</param>
        /// <returns>Access token</returns>
        public async Task<Account> LoginAsync(string username, string password)
        {
            Account account = null;

            using (var dbConnection = new OracleConnection(WebConfig.ConnectionString))
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("P_USERNAME", OracleDbType.Varchar2, ParameterDirection.Input, username);
                dyParam.Add("RSOUT", OracleDbType.RefCursor, ParameterDirection.Output);
                var query = "QTRR_ADMIN.GET_ACCOUNT";
                var obj = await SqlMapper.QueryAsync<Account>(dbConnection, query, param: dyParam, commandType: CommandType.StoredProcedure);
                account = (obj != null || obj.Count() == 0) ? obj.FirstOrDefault() : null;
            }

            return account;
        }

        /// <summary>
        /// Get modules that the user is allowed
        /// </summary>
        /// <param name="idNhanVien">The user's id</param>
        /// <returns>A task</returns>
        public async Task<IEnumerable<Menu>> GetModules(int idNhanVien)
        {
            try
            {
                using (IDbConnection conn = new OracleConnection(WebConfig.ConnectionString))
                {
                    var dyParam = new OracleDynamicParameters();
                    dyParam.Add("P_ID_NHANVIEN", OracleDbType.Varchar2, ParameterDirection.Input, idNhanVien.ToString());
                    dyParam.Add("RSOUT", OracleDbType.RefCursor, ParameterDirection.Output);
                    var query = "QTRR_ADMIN.GET_MENU";

                    IEnumerable<Menu> obj = await SqlMapper.QueryAsync<Menu>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
                    return obj;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
