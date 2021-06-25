using System;
using LetsCode.Kanban.Application.Core;

namespace LetsCode.Kanban.WebApi.ApplicationImplementations.Core
{
    public class UtcDateTime : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}