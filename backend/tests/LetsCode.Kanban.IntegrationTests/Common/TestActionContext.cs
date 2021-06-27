using System.Threading;
using LetsCode.Kanban.Application.Core;
using LetsCode.Kanban.Application.Models;

namespace LetsCode.Kanban.IntegrationTests.Common
{
    public class TestActionContext : IActionContext
    {
        public CancellationToken Cancel => CancellationToken.None;

        public CardData CardData => null;

        public void RegisterUserAction(Card card, UserAction userAction)
        {
            // noop
        }
    }
}