using System.Security.Authentication;
using System.Net;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using LetsCode.Kanban.Application.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using FluentValidation;

namespace LetsCode.Kanban.WebApi.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly IWebHostEnvironment _env;

        public ErrorHandlingMiddleware(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)GetStatusCode(ex);
                await context.Response.WriteAsJsonAsync(new {
                    message = GetMessage(ex),
                    detailed = GetDetailedException(ex),
                });
            }
        }

        private HttpStatusCode GetStatusCode(Exception ex) {
            return ex switch {
                ValidationException => HttpStatusCode.BadRequest,
                NotFoundException => HttpStatusCode.NotFound,
                InvalidCredentialException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };
        }

        private string GetMessage(Exception ex) {
            return ex switch {
                ValidationException validationException => validationException.Message,
                NotFoundException => "The resourece you requested could not be found.",
                InvalidCredentialException => "The credentials you provided are not valid to login.",
                _ => _env.IsDevelopment() ? ex.Message : "Unexpected error"
            };
        }

        private string GetDetailedException(Exception ex) {
            return _env.IsDevelopment() ? ex.ToString() : null;
        }
    }
}