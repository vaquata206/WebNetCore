using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebClient.Core.Helper;
using WebClient.Repositories.Interfaces;

namespace WebClient.Repositories.Implements
{
    /// <summary>
    /// Account repository
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        /// <summary>
        /// Http helper
        /// </summary>
        private HttpHelper httpHelper;

        /// <summary>
        /// A constructor
        /// </summary>
        public AccountRepository()
        {
            httpHelper = new HttpHelper();
        }

        /// <summary>
        /// Login the user
        /// </summary>
        /// <param name="username">The usename</param>
        /// <param name="password">The password</param>
        /// <returns>Access token</returns>
        public async Task<string> LoginAsync(string username, string password)
        {
            var token = string.Empty;

            // Login to another server
            var response = await httpHelper.PostAsync(JsonConvert.SerializeObject(new
            {
                username = username,
                password = password
            }), "login");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                token = "Bearer " + (await response.Content.ReadAsStringAsync()).Replace("\"","");
            }

            return token;
        }
    }
}
