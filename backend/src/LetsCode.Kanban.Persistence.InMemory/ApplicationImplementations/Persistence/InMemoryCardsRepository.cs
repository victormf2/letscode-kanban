using System;
using System.Threading.Tasks;
using LetsCode.Kanban.Application.Core;
using LetsCode.Kanban.Application.Models;
using LetsCode.Kanban.Application.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LetsCode.Kanban.Persistence.InMemory.ApplicationImplementations.Persistence
{
    public class InMemoryCardsRepository : ICardsRepository
    {
        private readonly InMemoryDbContext _dbContext;
        private readonly IActionContext _ctx;

        public InMemoryCardsRepository(
            InMemoryDbContext dbContext,
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

        public Task<Card> Find(Guid id)
        {
            return _dbContext.Set<Card>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id, _ctx.Cancel);
        }

        public Task Update(Card card)
        {
            _dbContext.Update(card);

            return _dbContext.SaveChangesAsync(_ctx.Cancel);
        }
    }
}