using System.Security.Claims;
using System.Threading.Tasks;

namespace LetsCode.Kanban.Application.Authentication
{
    public interface IAuthenticator
    {
        Task<ClaimsIdentity> Authenticate(string username, string password);
    }
}