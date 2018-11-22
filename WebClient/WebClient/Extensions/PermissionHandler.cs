using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using WebClient.Services.Interfaces;

namespace WebClient.Extensions
{
    /// <summary>
    /// Permission handler
    /// </summary>
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        /// <summary>
        /// Account service
        /// </summary>
        private IAccountService accountService;

        /// <summary>
        /// The logger
        /// </summary>
        private ILogger logger;

        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="accountService">Account service</param>
        /// <param name="logger">The logger</param>
        public PermissionHandler(IAccountService accountService, ILogger logger)
        {
            this.accountService = accountService;
            this.logger = logger;
        }

        /// <summary>
        /// Handler requirement
        /// </summary>
        /// <param name="context">Authorization handler context</param>
        /// <param name="requirement">Permission requirement</param>
        /// <returns>A completed task is returned</returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            // this.accountService.CheckUserPermission()
            try
            {
                var filterContext = (AuthorizationFilterContext)context.Resource;

                // Get the path of current request
                var path = requirement.IsModelUri ? 
                    filterContext.HttpContext.Request.Path.ToString() : 
                    filterContext.RouteData.Values["controller"] + "/" + filterContext.RouteData.Values["action"];

                if (this.accountService.CheckUserPermission(path, requirement.IsModelUri).Result)
                {
                    context.Succeed(requirement);
                }
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
            }

            return Task.CompletedTask;
        }
    }
}
