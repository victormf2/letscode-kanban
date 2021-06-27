using LetsCode.Kanban.Application.UserActions.AddCard;
using FluentValidation;
using LetsCode.Kanban.Application.UserActions.UpdateCard;
using LetsCode.Kanban.Application.UserActions.RemoveCard;
using LetsCode.Kanban.Application.UserActions.ListAllCards;
using LetsCode.Kanban.Application.UserActions.Login;
using Microsoft.Extensions.Configuration;
using LetsCode.Kanban.Application.Authentication.Implementation;
using LetsCode.Kanban.Application.Authentication;
using LetsCode.Kanban.Application.Core;
using LetsCode.Kanban.Application.Core.Implementation;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDateTime, UtcDateTime>();
            
            services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);

            services.AddScoped<AddCardAction>();
            services.AddScoped<UpdateCardAction>();
            services.AddScoped<RemoveCardAction>();
            services.AddScoped<ListAllCardsAction>();
            services.AddScoped<LoginAction>();

            var authenticationConfigurationSection = configuration.GetSection("Authentication");
            services.Configure<LocalEnvironmentAuthenticatorOptions>(authenticationConfigurationSection);
            services.AddSingleton<IAuthenticator, LocalEnvironmentAuthenticator>();

            var jwtConfigurationSection = configuration.GetSection("Jwt");
            services.Configure<JwtGeneratorOptions>(jwtConfigurationSection, options =>
            {
                options.BindNonPublicProperties = true;
            });
            services.AddSingleton<IJwtGenerator, JwtGenerator>();
        }
    }
}