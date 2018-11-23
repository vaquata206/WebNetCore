using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebClient.Core.Entities;

namespace WebClient.Services.Interfaces
{
    /// <summary>
    /// Account service
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Login the user
        /// </summary>
        /// <param name="username">The username of account</param>
        /// <param name="password">The user's password</param>
        Task<Account> LoginAsync(string username, string password);

        /// <summary>
        /// Get modules
        /// </summary>
        /// <param name="idNhanVien">The user's id</param>
        /// <returns>List of module</returns>
        Task<IEnumerable<Menu>> GetModulesAsync(int idNhanVien);
    }
}
