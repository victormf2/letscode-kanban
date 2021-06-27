using LetsCode.Kanban.Application.Persistence;
using LetsCode.Kanban.Persistence.EntityFrameworkCore;
using LetsCode.Kanban.Persistence.EntityFrameworkCore.ApplicationImplementations.Persistence;
using LetsCode.Kanban.Persistence.EntityFrameworkCore.InMemory;
using LetsCode.Kanban.Persistence.EntityFrameworkCore.PostgreSql;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInMemoryPersistence(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext, InMemoryApplicationDbContext>();

            services.AddRepositories();
        }

        public static void AddPostgreSqlPersistence(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext, PostgreSqlApplicationDbContext>();

            services.AddRepositories();
        }

        private static void AddRepositories(this IServiceCollection services)
        {

            services.AddScoped<ICardsRepository, CardsRepository>();
        }
    }
}