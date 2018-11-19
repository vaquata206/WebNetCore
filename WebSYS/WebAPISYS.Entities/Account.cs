using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPISYS.Entities
{
    /// <summary>
    /// User model
    /// </summary>
    public class Account
    {
        /// <summary>
        /// A string is name of user
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email of user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Birthday of user
        /// </summary>
        public DateTime Birthdate { get; set; }
    }
}
