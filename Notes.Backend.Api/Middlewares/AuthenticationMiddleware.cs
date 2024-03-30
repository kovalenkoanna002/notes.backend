using Microsoft.AspNetCore.Authentication;
using Notes.Backend.Application.Resources;
using Skreet2k.Common.Models;

namespace Notes.Backend.Api.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Method.Equals("OPTIONS") && context.Request.Path.Value.Contains("secure"))
            {
                var authenticateResult = await context.AuthenticateAsync();
                if (authenticateResult?.Failure != null)
                {
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";

                    var result = new Result()
                    {
                        ErrorMessage = authenticateResult.Failure.Message,
                        ReturnCode = (int)ErrorCode.AuthenticationFailed401,
                    };

                    await context.Response.WriteAsJsonAsync(result);
                    return;
                }
            }

            await _next(context);
        }
    }
}
