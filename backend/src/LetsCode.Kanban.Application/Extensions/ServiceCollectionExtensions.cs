using LetsCode.Kanban.Application.UserActions.AddCard;
using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);

            services.AddScoped<AddCardAction>();
        }
    }
}