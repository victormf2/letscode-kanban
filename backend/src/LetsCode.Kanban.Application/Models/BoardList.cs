using System.Collections.Generic;
namespace LetsCode.Kanban.Application.Models
{
    public class BoardList
    {
        public static readonly string ToDo = "ToDo";
        public static readonly string Doing = "Doing";
        public static readonly string Done = "Done";
        public static readonly IReadOnlyList<string> Ids = new[] { ToDo, Doing, Done };
    }
}