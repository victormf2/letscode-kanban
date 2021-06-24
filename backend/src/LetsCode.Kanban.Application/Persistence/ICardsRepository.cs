using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using LetsCode.Kanban.Application.Models;

namespace LetsCode.Kanban.Application.Persistence
{
    public interface ICardsRepository
    {
         Task<Guid> Add(Card card);
         Task<Card> Find(Guid id);
         Task Update(Card card);
         Task Remove(Card card);
         Task<IReadOnlyList<Card>> ListAll();
    }
}