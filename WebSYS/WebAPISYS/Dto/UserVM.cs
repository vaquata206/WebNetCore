using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPISYS.Dto
{
    /// <summary>
    /// User viewmodel
    /// </summary>
    public class UserVM
    {
        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password of user
        /// </summary>
        public string PassWord { get; set; }
    }
}
