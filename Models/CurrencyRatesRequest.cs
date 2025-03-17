namespace CurrencyConverterAPI.Models
{
    public class CurrencyRatesRequest
    {
        public string Base { get; set; }
        public string Date { get; set; }

        public string symbols { get; set; }
        public string ToDate { get; set; }
    }
    public class CurrencyData
    {
        public string CurrencyName { get; set; }
        public string Rate { get; set; }

        public List<CurrencyData> GetCurrencyData(string BaseCurrency)
        {
            List<CurrencyData> lst = new List<CurrencyData>();
            if (BaseCurrency == "EUR")
            {
                lst = new List<CurrencyData>
                {
                    new CurrencyData { CurrencyName = "USD", Rate = "1.0845" },
                    new CurrencyData { CurrencyName = "GBP", Rate = "0.8763" },
                    new CurrencyData { CurrencyName = "JPY", Rate = "141.27" },
                    new CurrencyData { CurrencyName = "AUD", Rate = "1.6810" },
                    new CurrencyData { CurrencyName = "CAD", Rate = "1.4450" },
                    new CurrencyData { CurrencyName = "CHF", Rate = "1.0872" },
                    new CurrencyData { CurrencyName = "CNY", Rate = "7.5421" },
                    new CurrencyData { CurrencyName = "INR", Rate = "90.77" }
                };
            }
            if (BaseCurrency == "USD")
            {
                lst = new List<CurrencyData>
                {
                    new CurrencyData { CurrencyName = "EUR", Rate = "0.8845" },
                    new CurrencyData { CurrencyName = "GBP", Rate = "0.8763" },
                    new CurrencyData { CurrencyName = "JPY", Rate = "135.27" },
                    new CurrencyData { CurrencyName = "AUD", Rate = "1.3810" },
                    new CurrencyData { CurrencyName = "CAD", Rate = "1.1450" },
                    new CurrencyData { CurrencyName = "CHF", Rate = "0.0872" },
                    new CurrencyData { CurrencyName = "CNY", Rate = "6.5421" },
                    new CurrencyData { CurrencyName = "INR", Rate = "80.77" }
                };
            }
            return lst;
        }
    }
}
