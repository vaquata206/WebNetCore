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
        /// Get the current user
        /// </summary>
        User CurrentUser { get; }

        /// <summary>
        /// Check user permission if the current user is allowed working with a action mapping the path
        /// </summary>
        /// <param name="path">The path of action</param>
        /// <param name="isModeUri">True: path is a uri. False: path is [controller]/[action]</param>
        /// <returns>return true if the current user is allowed</returns>
        Task<bool> CheckUserPermission(string path, bool isModelUri);
    }
}
