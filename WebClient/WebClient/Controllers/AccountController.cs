using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebClient.Models;

namespace WebClient.Controllers
{
    /// <summary>
    /// Account controller
    /// </summary>
    public class AccountController : Controller
    {
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
        public IActionResult Login(LoginViewModel login)
        {
            return this.Redirect("/");
        }
    }
}