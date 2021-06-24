using System;

namespace LetsCode.Kanban.Application.Models
{
    public class Card
    {
        public Guid Id { get; private set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string List { get; set; }
    }
}