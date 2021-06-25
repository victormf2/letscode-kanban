using LetsCode.Kanban.Application.UserActions.AddCard;
using FluentValidation;
using LetsCode.Kanban.Application.UserActions.UpdateCard;
using LetsCode.Kanban.Application.UserActions.RemoveCard;
using LetsCode.Kanban.Application.UserActions.ListAllCards;
using LetsCode.Kanban.Application.UserActions.Login;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);

            services.AddScoped<AddCardAction>();
            services.AddScoped<UpdateCardAction>();
            services.AddScoped<RemoveCardAction>();
            services.AddScoped<ListAllCardsAction>();
            services.AddScoped<LoginAction>();
        }
    }
}