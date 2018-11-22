using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        /// A http context
        /// </summary>
        private IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="account"></param>
        public AccountService(IAccountRepository account, IHttpContextAccessor httpContextAccessor)
        {
            this.account = account;
            this.httpContextAccessor = httpContextAccessor;
        }

        public User CurrentUser {
            get
            {
                var claims = httpContextAccessor.HttpContext.User.Claims;
                if (claims == null || claims.Count() == 0)
                {
                    return null;
                }

                return new User
                {
                    Username = claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).SingleOrDefault(),
                    Token = claims.Where(x => x.Type == "token").Select(x => x.Value).SingleOrDefault()
                };
            }
        }

        /// <summary>
        /// Login the user
        /// </summary>
        /// <param name="username">The username of account</param>
        /// <param name="password">The user's password</param>
        public async Task<string> LoginAsync(string username, string password)
        {
            var token =  await this.account.LoginAsync(username, password);
            
            return token;
        }

        /// <summary>
        /// Get modules
        /// </summary>
        /// <returns>List of module</returns>
        public IEnumerable<string> GetModules()
        {
            return new List<string> { "Home/About" };
        }
    }
}
