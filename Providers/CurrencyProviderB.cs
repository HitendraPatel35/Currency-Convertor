using CurrencyConverterAPI.Models;

namespace CurrencyConverterAPI.Providers
{
    public class CurrencyProviderB : ICurrencyProvider
    {
        public async Task<CurrencyResponse> GetConversionRateAsync(CurrencyRequest request)
        {
            // Simulate an asynchronous operation
            await Task.Delay(100);

            // Implement provider-specific logic here
            return new CurrencyResponse
            {
                FromCurrency = request.FromCurrency,
                ToCurrency = request.ToCurrency,
                Amount = request.Amount,
                ConvertedAmount = request.Amount * 1.2m // Example conversion rate
            };
        }

        public Task<CurrencyRatesResponse> GetCurrencyConversionRateAsync(CurrencyRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
