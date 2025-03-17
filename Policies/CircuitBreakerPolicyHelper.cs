using CurrencyConverterAPI.Models;
using Polly.CircuitBreaker;
using Polly;

namespace CurrencyConverterAPI.Policies
{
    public static class CircuitBreakerPolicyHelper
    {
        public static AsyncCircuitBreakerPolicy GetCircuitBreakerPolicy()
        {
            return Policy.Handle<Exception>()
                .CircuitBreakerAsync(
                    exceptionsAllowedBeforeBreaking: 3,
                    durationOfBreak: TimeSpan.FromSeconds(30),
                    onBreak: (exception, timespan) =>
                    {
                        // Log the circuit break
                        Console.WriteLine($"Circuit broken! Breaking for {timespan}. Error: {exception.Message}");
                    },
                    onReset: () =>
                    {
                        // Log the circuit reset
                        Console.WriteLine("Circuit reset!");
                    },
                    onHalfOpen: () =>
                    {
                        // Log the circuit half-open state
                        Console.WriteLine("Circuit half-open. Next call is a trial.");
                    });
        }
    }
}
