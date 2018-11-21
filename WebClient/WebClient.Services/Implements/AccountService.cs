﻿using System.Threading.Tasks;
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
        public async Task<string> LoginAsync(string username, string password)
        {
            var token =  await this.account.LoginAsync(username, password);

            return token;
        }
    }
}