using System;
using System.Collections.Generic;
using System.Text;

namespace WebClient.Core.Entities
{
    /// <summary>
    /// User class
    /// </summary>
    public class User
    {
        /// <summary>
        /// User name
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Access token
        /// </summary>
        public string Token { get; set; }
    }
}
