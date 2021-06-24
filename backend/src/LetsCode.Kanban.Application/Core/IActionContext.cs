using System.Threading;

namespace LetsCode.Kanban.Application.Core
{
    public interface IActionContext
    {
         CancellationToken Cancel { get; }
    }
}