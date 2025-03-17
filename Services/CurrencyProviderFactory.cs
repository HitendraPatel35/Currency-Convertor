using CurrencyConverterAPI.Models;
using CurrencyConverterAPI.Providers;

namespace CurrencyConverterAPI.Services
{
    public class CurrencyProviderFactory : ICurrencyProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CurrencyProviderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public ICurrencyProvider GetCurrencyProvider(string provider)
        {
            return provider switch
            {
                "ProviderA" => _serviceProvider.GetService<CurrencyProviderA>(),
                "ProviderB" => _serviceProvider.GetService<CurrencyProviderB>(),
                _ => throw new ArgumentException("Invalid provider")
            };
        }
        public ICurrencyRateProvider GetCurrencyRateProvider(string BaseCurrency)
        {
            return BaseCurrency switch
            {
                "EUR" => _serviceProvider.GetService<CurrencyProviderEUR>(),
                "USD" => _serviceProvider.GetService<CurrencyProviderUSD>(),
                _ => throw new ArgumentException("Invalid BaseCurrency")
            };
        }
    }
}
