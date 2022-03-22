using System;
using Microsoft.AspNetCore.Builder;

namespace My2Cents.API.Middlewares.Implements
{
    public static class AccessTokenMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenManagerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AccessTokenManagerMiddleware>();
        }
    }
}