using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPISYS.Services.Interfaces
{
    /// <summary>
    /// Account service interface
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Authenticate the user
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="passWord">User's password</param>
        /// <returns>The access token</returns>
        string Authenticate(string userName, string passWord);
    }
}
