

using CurrencyConverterAPI.Models;
using CurrencyConverterAPI.Policies;
using CurrencyConverterAPI.Providers;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace CurrencyConverterAPI.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyProvider _currencyProvider; 
        private readonly ICurrencyRateProvider _currencyRateProvider; 
        private readonly AsyncRetryPolicy<CurrencyResponse> _retryPolicy;
        private readonly AsyncRetryPolicy<CurrencyRatesResponse> _rateretryPolicy;
        private readonly AsyncCircuitBreakerPolicy _circuitBreakerPolicy;

        public CurrencyService(ICurrencyProvider currencyProvider, ICurrencyRateProvider currencyRateProvider)
        {
            _currencyProvider = currencyProvider;
            _retryPolicy = RetryPolicyHelper.GetRetryPolicy();
            _circuitBreakerPolicy = CircuitBreakerPolicyHelper.GetCircuitBreakerPolicy();
            _rateretryPolicy = RetryPolicyHelper.GetRateRetryPolicy();
            _currencyRateProvider = currencyRateProvider;
        }

        //public async Task<CurrencyResponse> ConvertCurrencyAsync(CurrencyRequest request)
        //{
        //    return await _currencyProvider.GetConversionRateAsync(request);
        //}
        public async Task<CurrencyResponse> ConvertCurrencyAsync(CurrencyRequest request)
        {
            return await _circuitBreakerPolicy.ExecuteAsync(() =>
                _retryPolicy.ExecuteAsync(() => _currencyProvider.GetConversionRateAsync(request))
            );
        }
        public async Task<CurrencyRatesResponse> ConvertCurrencyRateAsync(CurrencyRatesRequest request)
        {
            return await _circuitBreakerPolicy.ExecuteAsync(() =>
                _rateretryPolicy.ExecuteAsync(() => _currencyRateProvider.GetCurrencyConversionRateAsync(request))
            );
        }
    }
}
