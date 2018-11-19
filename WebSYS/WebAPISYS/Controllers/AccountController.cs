using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        /// Account service interface
        /// </summary>
        private IAccountService accountService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="accountService">account service</param>
        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        /// <summary>
        /// API login
        /// </summary>
        /// <param name="uservm">User viewmodel</param>
        /// <returns>A action result of the api</returns>
        [HttpPost]
        [Route("/login")]
        public IActionResult Login(UserVM uservm)
        {
            return this.Ok();
        }
    }
}