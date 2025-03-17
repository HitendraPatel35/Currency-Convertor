using CurrencyConverterAPI.Models;

namespace CurrencyConverterAPI.Services
{
    public interface ICurrencyService
    {
        Task<CurrencyResponse> ConvertCurrencyAsync(CurrencyRequest request);
        Task<CurrencyRatesResponse> ConvertCurrencyRateAsync(CurrencyRatesRequest request);
    }
}
