using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using WebClient.Core.Entities;
using WebClient.Models;
using WebClient.Services.Interfaces;

namespace WebClient.Controllers
{
    /// <summary>
    /// Account controller
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// account service
        /// </summary>
        private IAccountService accountService;

        /// <summary>
        /// The logger
        /// </summary>
        private ILogger logger;

        /// <summary>
        /// A contrustor
        /// </summary>
        /// <param name="accountService">account service</param>
        /// <param name="logger">The logger</param>
        public AccountController(IAccountService accountService, ILogger logger)
        {
            this.accountService = accountService;
            this.logger = logger;
        }

        /// <summary>
        /// Action login
        /// </summary>
        /// <returns>The login page</returns>
        [HttpGet("/login")]
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Action login
        /// </summary>
        /// <param name="login">Login info</param>
        /// <returns>Redirect to home page</returns>
        [HttpPost("/login")]
        public async Task<IActionResult> LoginAsync(LoginViewModel login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.MessageError = "Username or password is wrong";
                    return this.View("index");
                }

                var token = await this.accountService.LoginAsync(login.Username, login.Password);

                if (string.IsNullOrEmpty(token))
                {
                    ViewBag.MessageError = "Username or password is wrong";
                    return this.View("index");
                }

                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, login.Username),
                    new Claim("token", token)
                };

                // create identity
                ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");

                // create principal
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                        scheme: CookieAuthenticationDefaults.AuthenticationScheme,
                        principal: principal,
                        properties: new AuthenticationProperties
                        {
                        // IsPersistent = true, // for 'remember me' feature
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                        });

                this.TempData["StatusMessage"] = "Đăng nhập thành công";
                return this.Redirect("/");
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Action logout
        /// </summary>
        /// <returns>Redirect to login page</returns>
        [Authorize]
        [HttpGet("/logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return this.Redirect("/login");
        }
    }
}