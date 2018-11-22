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
        Task<string> LoginAsync(string username, string password);

        /// <summary>
        /// Get modules
        /// </summary>
        /// <returns>List of module</returns>
        IEnumerable<string> GetModules();
    }
}
