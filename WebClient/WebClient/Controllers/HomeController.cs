using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebClient.Core.Entities;
using WebClient.Core.Paging;
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
        /// <param name="request">paging request</param>
        /// <returns>khong biet</returns>
        [HttpPost]
        public IActionResult TestTable(PagingRequest request)
        {
            var list = this.Datatest();
            var data = list.Skip(request.Start).Take(request.Length).ToList();

            return this.Json(new PagingResponse<Account>()
            {
                Data = data,
                Draw = request.Draw,
                RecordsFiltered = list.Count(),
                RecordsTotal = list.Count(),
            });
        }

        /// <summary>
        /// Du lieu test
        /// </summary>
        /// <returns>du lieu</returns>
        public IEnumerable<Account> Datatest()
        {
            var list = new List<Account>();
            for (var i = 0; i < 1000; i++)
            {
                list.Add(new Account
                {
                    Ho_ten = "Account " + i,
                    Id_NhanVien = i,
                    Ma_NhanVien = "Ma" + i,
                    Ten_DangNhap = "Ten" + i
                });
            }

            return list;
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
