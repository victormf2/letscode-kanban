using System;

namespace LetsCode.Kanban.Application.UserActions.AddCard
{
    public class AddCardResult
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Content { get; init; }
        public string ListId { get; init; }
    }
}