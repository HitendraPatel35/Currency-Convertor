using CurrencyConverterAPI.Models;

namespace CurrencyConverterAPI.Providers
{
    public interface ICurrencyProvider
    {
        Task<CurrencyResponse> GetConversionRateAsync(CurrencyRequest request);
        Task<CurrencyRatesResponse> GetCurrencyConversionRateAsync(CurrencyRequest request);
    }
}
