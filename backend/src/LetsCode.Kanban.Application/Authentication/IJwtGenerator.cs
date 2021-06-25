using System.Security.Claims;
using System.Threading.Tasks;

namespace LetsCode.Kanban.Application.Authentication
{
    public interface IJwtGenerator
    {
        Task<string> GenerateJwtFor(ClaimsIdentity identity);
    }
}