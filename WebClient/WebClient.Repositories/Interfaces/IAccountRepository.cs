using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebClient.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<string> LoginAsync(string username, string password);
    }
}
