using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LetsCode.Kanban.WebApi.Swagger.OperationFilters
{
    public class AddSecurityRequirementOperationFilter : IOperationFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddSecurityRequirementOperationFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var endpointMetadata = context.ApiDescription.ActionDescriptor.EndpointMetadata;
            var isAuthorized = endpointMetadata.Any(c => c is AuthorizeAttribute);
            var allowAnonymous = endpointMetadata.Any(c => c is AllowAnonymousAttribute);

            if (isAuthorized && !allowAnonymous)
            {
                if (operation.Security == null)
                {
                    operation.Security = new List<OpenApiSecurityRequirement>();
                }

                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            }
                        },
                        new List<string>()
                    }
                });
            }
        }
    }
}