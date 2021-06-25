using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LetsCode.Kanban.Persistence.EntityFrameworkCore.InMemory
{
    public class InMemoryApplicationDbContext : ApplicationDbContext
    {
        private readonly InMemoryDatabaseOptions _inMemoryDatabaseOptions;
        public InMemoryApplicationDbContext(
            DbContextOptions<InMemoryApplicationDbContext> options,
            IOptions<InMemoryDatabaseOptions> inMemoryDatabaseOptions) : base(options)
        {
            _inMemoryDatabaseOptions = inMemoryDatabaseOptions.Value;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseInMemoryDatabase(_inMemoryDatabaseOptions.Name);
        }
    }
}