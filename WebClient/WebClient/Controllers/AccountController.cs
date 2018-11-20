using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
    }
}