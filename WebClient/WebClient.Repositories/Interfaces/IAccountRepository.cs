using System.Collections.Generic;
using System.Threading.Tasks;
using WebClient.Core.Entities;

namespace WebClient.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Login with the user
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns>A Account of the current user</returns>
        Task<Account> LoginAsync(string username, string password);

        /// <summary>
        /// Get modules that the user is allowed
        /// </summary>
        /// <param name="idNhanVien">The user's id</param>
        /// <returns>A task</returns>
        Task<IEnumerable<Menu>> GetModules(int idNhanVien);
    }
}
