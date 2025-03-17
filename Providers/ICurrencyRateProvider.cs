using CurrencyConverterAPI.Models;

namespace CurrencyConverterAPI.Providers
{
    public interface ICurrencyRateProvider
    {
        Task<CurrencyRatesResponse> GetCurrencyConversionRateAsync(CurrencyRatesRequest request);
    }
}
