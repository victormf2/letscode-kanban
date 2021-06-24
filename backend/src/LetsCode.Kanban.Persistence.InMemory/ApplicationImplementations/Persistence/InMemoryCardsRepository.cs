using System;
using System.Threading.Tasks;
using LetsCode.Kanban.Application.Core;
using LetsCode.Kanban.Application.Models;
using LetsCode.Kanban.Application.Persistence;

namespace LetsCode.Kanban.Persistence.InMemory.ApplicationImplementations.Persistence
{
    public class InMemoryCardsRepository : ICardsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IActionContext _ctx;

        public InMemoryCardsRepository(
            ApplicationDbContext dbContext,
            IActionContext ctx)
        {
            _dbContext = dbContext;
            _ctx = ctx;
        }

        public async Task<Guid> Add(Card card)
        {
            _dbContext.Add(card);
            await _dbContext.SaveChangesAsync(_ctx.Cancel);

            return card.Id;
        }
    }
}