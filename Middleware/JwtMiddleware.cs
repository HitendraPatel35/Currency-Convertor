﻿namespace CurrencyConverterAPI.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Implement JWT authentication logic here
            await _next(context);
        }
    }
}
