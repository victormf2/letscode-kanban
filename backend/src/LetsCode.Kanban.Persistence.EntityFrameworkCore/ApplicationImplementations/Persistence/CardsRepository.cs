using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LetsCode.Kanban.Application.Core;
using LetsCode.Kanban.Application.Models;
using LetsCode.Kanban.Application.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LetsCode.Kanban.Persistence.EntityFrameworkCore.ApplicationImplementations.Persistence
{
    public class CardsRepository : ICardsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IActionContext _ctx;

        public CardsRepository(
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

        public async Task<Card> Find(Guid id)
        {
            var card = await _dbContext.Set<Card>()
                .FindAsync(new object[] { id }, _ctx.Cancel);
            return card;
        }

        public async Task<IReadOnlyList<Card>> ListAll()
        {
            var cards = await _dbContext.Set<Card>()
                .AsNoTracking()
                .ToListAsync();

            return cards;
        }

        public Task Remove(Card card)
        {
            _dbContext.Remove(card);

            return _dbContext.SaveChangesAsync(_ctx.Cancel);
        }

        public Task Update(Card card)
        {
            var entry = _dbContext.Entry(card);
            if (entry.State == EntityState.Detached)
            {
                _dbContext.Update(card);
            }

            return _dbContext.SaveChangesAsync(_ctx.Cancel);
        }
    }
}