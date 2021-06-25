using LetsCode.Kanban.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace LetsCode.Kanban.Persistence.EntityFrameworkCore
{
    public abstract class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Card>();
        }
    }
}