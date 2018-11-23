using System.Collections.Generic;
using System.Threading.Tasks;
using WebClient.Core.Entities;
using WebClient.Repositories.Interfaces;
using WebClient.Services.Interfaces;

namespace WebClient.Services.Implements
{
    /// <summary>
    /// Account service
    /// </summary>
    public class AccountService: IAccountService
    {
        /// <summary>
        /// account repository
        /// </summary>
        private IAccountRepository account;

        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="account"></param>
        public AccountService(IAccountRepository account)
        {
            this.account = account;
        }

        /// <summary>
        /// Login the user
        /// </summary>
        /// <param name="username">The username of account</param>
        /// <param name="password">The user's password</param>
        public async Task<Account> LoginAsync(string username, string password)
        {
            var account =  await this.account.LoginAsync(username, password);
            
            return account;
        }

        /// <summary>
        /// Get modules
        /// </summary>
        /// <param name="idNhanVien">The user's id</param>
        /// <returns>List of module</returns>
        public async Task<IEnumerable<Menu>> GetModulesAsync(int idNhanVien)
        {
            return await this.account.GetModules(idNhanVien);
        }
    }
}
