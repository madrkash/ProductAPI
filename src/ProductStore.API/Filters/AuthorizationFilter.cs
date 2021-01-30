using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProductStore.API.Middleware;

namespace ProductStore.API.Filters
{
    public class AuthorizationFilterAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private const string ApiKeyName = "X-Api-Key";
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authorizationConfig = context.HttpContext.RequestServices.GetRequiredService<IOptions<AuthorizationConfig>>().Value;
            var apiKey = authorizationConfig.ApiKey;

            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyName, out var extractedApiKey))
            {
                PopulateUnauthorizedResponse(context, "Api Key was not provided");
            }
            else if(!apiKey.Equals(extractedApiKey))
            {
                PopulateUnauthorizedResponse(context, "Unauthorized client");
            }

            return Task.CompletedTask;
        }

        private static void PopulateUnauthorizedResponse(AuthorizationFilterContext context, string message)
        {
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
            context.Result = new UnauthorizedObjectResult(message);
        }
    }
}
