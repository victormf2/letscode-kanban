using System;

namespace LetsCode.Kanban.Application.UserActions.UpdateCard
{
    public class UpdateCardParameters
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Content { get; init; }
        public string ListId { get; init; }
    }
}