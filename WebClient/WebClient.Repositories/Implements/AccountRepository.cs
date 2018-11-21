using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebClient.Core;
using WebClient.Core.Helper;
using WebClient.Repositories.Helper;
using WebClient.Repositories.Interfaces;

namespace WebClient.Repositories.Implements
{
    public class AccountRepository : IAccountRepository
    {
        private HttpHelper httpHelper;
        public AccountRepository()
        {
            httpHelper = new HttpHelper();
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var token = string.Empty;

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
