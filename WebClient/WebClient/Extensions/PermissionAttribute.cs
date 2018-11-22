using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebClient.Extensions
{
    /// <summary>
    /// Permission attribute
    /// </summary>
    public class PermissionAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// A constructor without param.
        /// </summary>
        public PermissionAttribute()
        {
            this.IsModeUri = false;
        }

        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="isModeUri">Is mode uri</param>
        public PermissionAttribute(bool isModeUri)
        {
            this.IsModeUri = isModeUri;
        }

        /// <summary>
        /// Is mode uri
        /// </summary>
        public bool IsModeUri
        {
            set
            {
                this.Policy = value ? PermissionRequirement.PermissionPolicies.PermissionUri : PermissionRequirement.PermissionPolicies.Permission;
            }
        }
    }
}
