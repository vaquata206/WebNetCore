using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using WebClient.Models;

namespace WebClient.Controllers
{
    /// <summary>
    /// Home controller
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        /// <summary>
        /// N Logger
        /// </summary>
        private ILogger logger;

        /// <summary>
        /// Home controller
        /// </summary>
        /// <param name="logger">The logger</param>
        public HomeController(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Action: Home/
        /// </summary>
        /// <returns>Index page</returns>
        public IActionResult Index()
        {
            // var a = this.TempData["StatusMessage"];
            return this.View();
        }

        /// <summary>
        /// Action: Home/about
        /// </summary>
        /// <returns>About page</returns>
        public IActionResult About()
        {
            this.ViewData["Message"] = "Your application description page.";

            return this.View();
        }

        /// <summary>
        /// Action: Home/contact
        /// </summary>
        /// <returns>Contact page</returns>
        public IActionResult Contact()
        {
            this.ViewData["Message"] = "Your contact page.";

            return this.View();
        }

        /// <summary>
        /// Action: Home/privacy
        /// </summary>
        /// <returns>Privacy page</returns>
        public IActionResult Privacy()
        {
            return this.View();
        }

        /// <summary>
        /// Action: Home/error
        /// </summary>
        /// <returns>Error page</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel
            {
                RequestId = (Activity.Current == null) ? HttpContext.TraceIdentifier : Activity.Current.Id
            });
        }
    }
}
