using System.Threading;
using LetsCode.Kanban.Application.Core;

namespace LetsCode.Kanban.IntegrationTests.Common
{
    public class TestActionContext : IActionContext
    {
        public CancellationToken Cancel => CancellationToken.None;
    }
}