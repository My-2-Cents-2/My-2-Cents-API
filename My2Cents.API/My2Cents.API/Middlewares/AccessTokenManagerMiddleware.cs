using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using My2Cents.API.AuthenticationService.Interfaces;
using My2Cents.API.Middlewares.Interfaces;

namespace My2Cents.API.Middlewares.Implements
{
    public class AccessTokenManagerMiddleware : IAccessTokenManagerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IAccessTokenManager accessTokenManager;

        public AccessTokenManagerMiddleware(RequestDelegate next, IAccessTokenManager accessTokenManager)
        {
            this.next = next;
            this.accessTokenManager = accessTokenManager;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (await accessTokenManager.IsCurrentActiveToken())
            {
                await next(context);

                return;
            }
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}