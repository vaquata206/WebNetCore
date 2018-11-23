using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebClient.Extensions;
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
        /// Logger Homecontroller
        /// </summary>
        private ILogger<HomeController> logger;

        /// <summary>
        /// Home controller
        /// </summary>
        /// <param name="logger">The logger</param>
        public HomeController(ILogger<HomeController> logger)
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
        /// Test function
        /// </summary>
        /// <returns>khong biet</returns>
        [HttpPost]
        public string TestTable()
        {
            return "aaaa";
        }

        /// <summary>
        /// Action: Home/about
        /// </summary>
        /// <returns>About page</returns>
        [Permission(true)]
        public IActionResult About()
        {
            this.ViewData["Message"] = "Your application description page.";

            return this.View();
        }

        /// <summary>
        /// Action: Home/contact
        /// </summary>
        /// <returns>Contact page</returns>
        [Permission]
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
    }
}
