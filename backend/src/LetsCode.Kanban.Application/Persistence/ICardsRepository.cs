using System;
using System.Threading.Tasks;
using LetsCode.Kanban.Application.Models;

namespace LetsCode.Kanban.Application.Persistence
{
    public interface ICardsRepository
    {
         Task<Guid> Add(Card card);
    }
}