﻿using CurrencyConverterAPI.Models;

namespace CurrencyConverterAPI.Providers
{
    public class CurrencyProviderUSD : ICurrencyRateProvider
    {
        public async Task<CurrencyRatesResponse> GetCurrencyConversionRateAsync(CurrencyRatesRequest request)
        {
            // Simulate an asynchronous operation
            await Task.Delay(100);

            CurrencyData obj = new CurrencyData();
            CurrencyRatesResponse model = new CurrencyRatesResponse();
            var Ratedata = obj.GetCurrencyData("USD");
            model.Base = "USD";
            model.Date = DateTime.UtcNow.ToString("yyyy-MM-dd");
            if (request.symbols != "")
            {
                List<string> items = request.symbols.Split(',').ToList();
                Ratedata = Ratedata.Where(rate => items.Contains(rate.CurrencyName)).ToList();
            }
            model.Rates = Ratedata;

            return model;
        }
    }
}
