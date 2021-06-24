using LetsCode.Kanban.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace LetsCode.Kanban.Persistence.InMemory
{
    public class InMemoryDbContext : DbContext
    {
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext>  options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Card>();
        }
    }
}