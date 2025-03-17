using CurrencyConverterAPI.Providers;

namespace CurrencyConverterAPI.Services
{
    public class CurrencyServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CurrencyServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICurrencyService GetCurrencyService(string provider)
        {
            try
            {
                ICurrencyProvider currencyProvider = provider switch
                {
                    "ProviderA" => _serviceProvider.GetService<CurrencyProviderA>(),
                    "ProviderB" => _serviceProvider.GetService<CurrencyProviderB>(),
                    _ => throw new ArgumentException("Invalid provider")
                };

                if (currencyProvider == null)
                {
                    throw new InvalidOperationException($"Currency provider '{provider}' could not be resolved.");
                }

                return new CurrencyService(currencyProvider,null);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ICurrencyService GetCurrencyRateService(string BaseCurrency)
        {
            try
            {
                ICurrencyRateProvider currencyProvider = BaseCurrency switch
                {
                    "EUR" => _serviceProvider.GetService<CurrencyProviderEUR>(),
                    "USD" => _serviceProvider.GetService<CurrencyProviderUSD>(),
                    _ => throw new ArgumentException("Invalid provider")
                };

                if (currencyProvider == null)
                {
                    throw new InvalidOperationException($"Currency provider '{BaseCurrency}' could not be resolved.");
                }

                return new CurrencyService(null,currencyProvider);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
