using System;
using System.Threading;
using LetsCode.Kanban.Application.Models;

namespace LetsCode.Kanban.Application.Core
{
    public interface IActionContext
    {
        CancellationToken Cancel { get; }
        CardData CardData { get; }
        void RegisterUserAction(Card card, UserAction userAction);
    }

    public class CardData
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string ListId { get; init; }
        public string Content { get; init; }
        public UserAction Action { get; init; }
        public DateTime Timestamp { get; init; }
    }

    public enum UserAction
    {
        Update,
        Remove
    }
}