using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    /// <summary>
    /// Error controller
    /// </summary>
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        /// <summary>
        /// Error404 page
        /// </summary>
        /// <returns>Page 404</returns>
        [Route("error/404")]
        public IActionResult Error404()
        {
            return this.View();
        }

        /// <summary>
        /// Error page
        /// </summary>
        /// <param name="code">Error code</param>
        /// <returns>Redirect to 401 page</returns>
        [Route("error/{code:int}")]
        public IActionResult Error(int code)
        {
            // handle different codes or just return the default error view
            return this.Redirect("error/404");
        }
    }
}
