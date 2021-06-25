using System;

namespace LetsCode.Kanban.Application.Core
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}