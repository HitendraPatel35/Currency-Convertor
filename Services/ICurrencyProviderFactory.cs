

using CurrencyConverterAPI.Providers;

namespace CurrencyConverterAPI.Services
{
    public interface ICurrencyProviderFactory
    {
        ICurrencyProvider GetCurrencyProvider(string provider);
        ICurrencyRateProvider GetCurrencyRateProvider(string provider);

    }
}
