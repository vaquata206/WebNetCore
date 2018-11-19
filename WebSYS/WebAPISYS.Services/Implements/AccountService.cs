using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebAPISYS.Entities;
using WebAPISYS.Services.Interfaces;

namespace WebAPISYS.Services.Implements
{
    /// <summary>
    /// Account service
    /// </summary>
    public class AccountService : IAccountService
    {
        /// <summary>
        /// Configuration root
        /// </summary>
        private IConfigurationRoot configuration;

        /// <summary>
        /// The contrustor
        /// </summary>
        /// <param name="configuration">IConfiguration root</param>
        public AccountService(IConfigurationRoot configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Authenticate the user
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="passWord">User's password</param>
        /// <returns>Access token</returns>
        public string Authenticate(string userName, string passWord)
        {
            try
            {
                // TODO : Check username and password is valid
                var user = new Account
                {
                    UserName = userName,
                    Name = "Test",
                    Email = "Test@test.com",
                    Birthdate = DateTime.Now
                };

                return this.BuildToken(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Build token from the account
        /// </summary>
        /// <param name="account">The account</param>
        /// <returns>The access token</returns>
        private string BuildToken(Account account)
        {
            // This is payload of token. Set user's info at here.
            var claims = new[] 
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.Name),
                new Claim(JwtRegisteredClaimNames.Email, account.Email),
                new Claim(JwtRegisteredClaimNames.Birthdate, account.Birthdate.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                // HACK: Set User's role is admin for test
                new Claim(ClaimTypes.Role, "Admin"),
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            int expires = 0;
            if (!int.TryParse(this.configuration["Jwt:Expires"], out expires))
            {
                // Default expire time is 30 min
                expires = 30;
            }

            var token = new JwtSecurityToken(
                this.configuration["Jwt:Issuer"],
                this.configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(expires),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
