namespace CurrencyConverterAPI.Logging
{
    public class LoggingService
    {
        private readonly ILogger<LoggingService> _logger;

        public LoggingService(ILogger<LoggingService> logger)
        {
            _logger = logger;
        }

        public void LogRequest(HttpContext context)
        {
            var clientIp = context.Connection.RemoteIpAddress?.ToString();
            var clientId = context.User.FindFirst("client_id")?.Value;
            var httpMethod = context.Request.Method;
            var endpoint = context.Request.Path;
            var responseCode = context.Response.StatusCode;
            var responseTime = DateTime.UtcNow; // Simplified for example

            _logger.LogInformation($"Client IP: {clientIp}, Client ID: {clientId}, HTTP Method: {httpMethod}, Endpoint: {endpoint}, Response Code: {responseCode}, Response Time: {responseTime}");
        }
    }
}
