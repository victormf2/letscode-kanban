using System;
using System.Collections.Generic;

namespace LetsCode.Kanban.Application.UserActions.RemoveCard
{
    public class RemoveCardResult
    {
        public IReadOnlyList<Card> Cards { get; init; }

        public class Card
        {
            public Guid Id { get; init; }
            public string Title { get; init; }
            public string Content { get; init; }
            public string ListId { get; init; }
        }
    }
}