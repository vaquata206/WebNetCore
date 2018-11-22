using System.Threading.Tasks;
using WebClient.Core.Entities;

namespace WebClient.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> LoginAsync(string username, string password);
    }
}
