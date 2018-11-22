﻿using Microsoft.AspNetCore.Authorization;

namespace WebClient.Extensions
{
    /// <summary>
    /// Permission requirement
    /// </summary>
    public class PermissionRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Is mode uri
        /// </summary>
        public bool IsModelUri { get; set; }

        /// <summary>
        /// Permission policies
        /// </summary>
        public static class PermissionPolicies
        {
            /// <summary>
            /// Permission policy
            /// </summary>
            public const string Permission = "PermissionUser";

            /// <summary>
            /// Permission uri policy
            /// </summary>
            public const string PermissionUri = "PermissionUserUri";
        }
    }
}
