using Bogus;
using LetsCode.Kanban.Application.Models;
using System.Linq;

namespace LetsCode.Kanban.TestHelpers.Extensions
{
    public static class BogusExtensions
    {
        public static string ListId(this Faker faker)
        {
            return faker.PickRandom(BoardList.Ids.AsEnumerable());
        }
    }
}