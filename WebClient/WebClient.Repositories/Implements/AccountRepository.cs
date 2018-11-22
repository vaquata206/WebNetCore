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
            var token = string.Empty;
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
    }
}
