using LetsCode.Kanban.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LetsCode.Kanban.Persistence.EntityFrameworkCore.PostgreSql
{
    public class PostgreSqlApplicationDbContext : ApplicationDbContext
    {
        private readonly PostgreSqlOptions _postgreSqlOptions;
        public PostgreSqlApplicationDbContext(
            DbContextOptions<PostgreSqlApplicationDbContext> options, 
            IOptions<PostgreSqlOptions> postgreSqlOptions) : base(options)
        {
            _postgreSqlOptions = postgreSqlOptions.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseNpgsql(_postgreSqlOptions.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Card>(card =>
            {
                card.ToTable("cards");

                card.Property(c => c.Title).IsRequired().HasMaxLength(50);
                card.Property(c => c.Content).IsRequired();
                card.Property(c => c.ListId).IsRequired();
            });
        }
    }
}