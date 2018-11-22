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
        private AuthHelper auth;

        /// <summary>
        /// The logger
        /// </summary>
        private ILogger logger;

        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="auth">Account service</param>
        /// <param name="logger">The logger</param>
        public PermissionHandler(AuthHelper auth, ILogger logger)
        {
            this.auth = auth;
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

                if (this.auth.CheckUserPermission(path, requirement.IsModelUri))
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
