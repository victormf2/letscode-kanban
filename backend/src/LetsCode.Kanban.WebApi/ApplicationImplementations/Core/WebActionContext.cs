using System.Threading;
using LetsCode.Kanban.Application.Core;
using Microsoft.AspNetCore.Http;

namespace LetsCode.Kanban.WebApi.ApplicationImplementations.Core
{
    public class WebActionContext : IActionContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CancellationToken Cancel => _httpContextAccessor.HttpContext.RequestAborted;

        public WebActionContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}