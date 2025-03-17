using CurrencyConverterAPI.Models;
using Polly;
using Polly.Retry;

namespace CurrencyConverterAPI.Policies
{
    public static class RetryPolicyHelper
    {
        public static AsyncRetryPolicy<CurrencyResponse> GetRetryPolicy()
        {
            try
            {
                return Policy.HandleResult<CurrencyResponse>(r => r == null)
                .Or<HttpRequestException>()
                .WaitAndRetryAsync(
                    retryCount: 5,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                    onRetry: (response, timespan, retryCount, context) =>
                    {
                        // Log the retry attempt
                        Console.WriteLine($"Retry {retryCount} encountered an error. Waiting {timespan} before next retry. Error: {response.Exception?.Message ?? "No response"}");
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine("API call failed after retries. Error: " + ex.Message);
                return null;
            }
        }
        public static AsyncRetryPolicy<CurrencyRatesResponse> GetRateRetryPolicy()
        {
            try
            {
                return Policy.HandleResult<CurrencyRatesResponse>(r => r == null)
                .Or<HttpRequestException>()
                .WaitAndRetryAsync(
                    retryCount: 5,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                    onRetry: (response, timespan, retryCount, context) =>
                    {
                        // Log the retry attempt
                        Console.WriteLine($"Retry {retryCount} encountered an error. Waiting {timespan} before next retry. Error: {response.Exception?.Message ?? "No response"}");
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine("API call failed after retries. Error: " + ex.Message);
                return null;
            }
        }
    }
}

