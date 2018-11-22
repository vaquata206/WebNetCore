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
using WebClient.Extensions;
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
        /// Auth helper
        /// </summary>
        private AuthHelper authHelper;

        /// <summary>
        /// A contrustor
        /// </summary>
        /// <param name="accountService">account service</param>
        /// <param name="logger">The logger</param>
        /// <param name="authHelper">Auth helper</param>
        public AccountController(IAccountService accountService, ILogger logger, AuthHelper authHelper)
        {
            this.accountService = accountService;
            this.logger = logger;
            this.authHelper = authHelper;
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
                    var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                    ViewBag.MessageError = message;
                    return this.View("index");
                }
                
                var account = await this.accountService.LoginAsync(login.Username, login.Password);

                if (account == null || string.IsNullOrEmpty(account.Ma_NhanVien))
                {
                    ViewBag.MessageError = "Tên đăng nhập hoặc mật khẩu không đúng";
                    return this.View("index");
                }

                // Login to cookie authentication
                await this.authHelper.LoginAsync(account);

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
            try
            {
                // Logout
                await this.authHelper.LogoutAsync();
                return this.Redirect("/login");
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                throw ex;
            }
        }
    }
}