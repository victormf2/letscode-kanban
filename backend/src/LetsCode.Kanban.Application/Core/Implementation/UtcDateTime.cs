using System;

namespace LetsCode.Kanban.Application.Core.Implementation
{
    public class UtcDateTime : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}