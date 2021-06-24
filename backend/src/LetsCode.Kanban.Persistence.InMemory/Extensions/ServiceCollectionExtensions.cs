using LetsCode.Kanban.Application.Persistence;
using LetsCode.Kanban.Persistence.InMemory;
using LetsCode.Kanban.Persistence.InMemory.ApplicationImplementations.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInMemoryPersistence(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("letscode_kanban");
            });

            services.AddScoped<ICardsRepository, InMemoryCardsRepository>();
        }
    }
}