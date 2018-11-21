using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    /// <summary>
    /// Login view model
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// The Username
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Is remember me
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
