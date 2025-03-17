using Microsoft.Extensions.Caching.Memory;

namespace CurrencyConverterAPI.Middleware
{
    public class ThrottlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;
        private readonly int _requestLimit;
        private readonly TimeSpan _timeWindow;

        public ThrottlingMiddleware(RequestDelegate next, IMemoryCache cache, int requestLimit, TimeSpan timeWindow)
        {
            _next = next;
            _cache = cache;
            _requestLimit = requestLimit;
            _timeWindow = timeWindow;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var clientIp = context.Connection.RemoteIpAddress?.ToString();
            if (clientIp == null)
            {
                await _next(context);
                return;
            }

            var cacheKey = $"RequestCount_{clientIp}";
            var requestCount = _cache.Get<int>(cacheKey);

            if (requestCount >= _requestLimit)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Too many requests. Please try again later.");
                return;
            }

            _cache.Set(cacheKey, requestCount + 1, _timeWindow);
            await _next(context);
        }
    }
}
