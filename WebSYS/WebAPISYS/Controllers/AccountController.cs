using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using WebAPISYS.Dto;
using WebAPISYS.Services.Interfaces;

namespace WebAPISYS.Controllers
{
    /// <summary>
    /// Account controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// Logger interface
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Account service interface
        /// </summary>
        private IAccountService accountService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="accountService">Account service</param>
        /// <param name="logger">The logger</param>
        public AccountController(IAccountService accountService, ILogger logger)
        {
            this.accountService = accountService;
            this.logger = logger;
        }

        /// <summary>
        /// API login
        /// </summary>
        /// <param name="loginModel">A viewmodel contain user's info to login the page. For example: username, passwork</param>
        /// <returns>A action result of the api</returns>
        [HttpPost]
        [Route("/login")]
        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                this.logger.Error("Index page says hello");
                IActionResult response = Unauthorized();

                // Validate the view model
                if (ModelState.IsValid)
                {
                    var token = this.accountService.Authenticate(loginModel.UserName, loginModel.PassWord);
                    response = this.Ok(token);
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Logout the current user
        /// </summary>
        /// <returns>A action result</returns>
        [Authorize]
        [HttpGet]
        [Route("/logout")]
        public IActionResult Logout()
        {
            try
            {
                return this.Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}