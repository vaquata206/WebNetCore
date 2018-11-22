using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using WebClient.Core.Entities;
using WebClient.Services.Interfaces;

namespace WebClient.Extensions
{
    /// <summary>
    /// Auth helper
    /// </summary>
    public class AuthHelper
    {
        /// <summary>
        /// Session key modules
        /// </summary>
        private const string SessionKeyModules = "modules";

        /// <summary>
        /// Account service
        /// </summary>
        private IAccountService accountService;

        /// <summary>
        /// Http context accessor
        /// </summary>
        private IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="accountService">Account service</param>
        /// <param name="httpContextAccessor">http context accessor</param>
        public AuthHelper(IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            this.accountService = accountService;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Get modules
        /// </summary>
        public IEnumerable<string> Modules
        {
            get
            {
                IEnumerable<string> modules = this.httpContextAccessor.HttpContext.Session.GetObject<IEnumerable<string>>(SessionKeyModules);

                if (modules == null || modules.Count() == 0)
                {
                    // Get modules from the service
                    modules = this.accountService.GetModules();

                    // Store modules to session
                    this.httpContextAccessor.HttpContext.Session.SetObject(SessionKeyModules, modules);
                }

                return modules;
            }
        }

        /// <summary>
        /// Get current user
        /// </summary>
        public Account CurrentUser
        {
            get
            {
                var claims = this.httpContextAccessor.HttpContext.User.Claims;
                if (claims == null || claims.Count() == 0)
                {
                    return null;
                }

                return new Account
                {
                   Ten_DangNhap = claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).SingleOrDefault(),
                   Ho_ten = claims.Where(x => x.Type == ClaimTypes.Name).Select(x => x.Value).SingleOrDefault(),
                   Ma_NhanVien = claims.Where(x => x.Type == ClaimTypes.Surname).Select(x => x.Value).SingleOrDefault(),
                   Id_NhanVien = int.Parse(claims.Where(x => x.Type == "id").Select(x => x.Value).SingleOrDefault())
                };
            }
        }

        /// <summary>
        /// Login with the account
        /// </summary>
        /// <param name="account">The account</param>
        /// <returns>A void task</returns>
        public async Task LoginAsync(Account account)
        {
            List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, account.Ten_DangNhap),
                    new Claim(ClaimTypes.Name, account.Ho_ten ?? string.Empty),
                    new Claim(ClaimTypes.Surname, account.Ma_NhanVien ?? string.Empty),
                    new Claim("id", account.Id_NhanVien.ToString())
                };

            // create identity
            ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");

            // create principal
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await this.httpContextAccessor.HttpContext.SignInAsync(
                    scheme: CookieAuthenticationDefaults.AuthenticationScheme,
                    principal: principal,
                    properties: new AuthenticationProperties
                    {
                        // IsPersistent = true, // for 'remember me' feature
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                    });
        }

        /// <summary>
        /// Logout current account
        /// </summary>
        /// <returns>A void task</returns>
        public async Task LogoutAsync()
        {
            // Logout current user
            await this.httpContextAccessor.HttpContext.SignOutAsync();

            // Remove all entities from current session
            this.httpContextAccessor.HttpContext.Session.Clear();
        }

        /// <summary>
        /// Check user permission if the current user is allowed working with a action mapping the path
        /// </summary>
        /// <param name="path">The path of action</param>
        /// <param name="isModeUri">True: path is a uri. False: path is [controller]/[action]</param>
        /// <returns>return true if the current user is allowed</returns>
        public bool CheckUserPermission(string path, bool isModeUri)
        {
            // HACK
            var pathUp = path.Trim('/').ToUpper();
            return this.Modules.Any(x => x.ToUpper() == pathUp);
        }
    }
}
