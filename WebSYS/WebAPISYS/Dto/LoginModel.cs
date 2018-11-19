using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPISYS.Dto
{
    /// <summary>
    /// User viewmodel
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// User name
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Password of user
        /// </summary>
        [Required]
        public string PassWord { get; set; }
    }
}
