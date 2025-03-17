namespace CurrencyConverterAPI.Models
{
    public class CurrencyRatesResponse
    {
        public string Base { get; set; }
        public string Date { get; set; }
        //public decimal Amount { get; set; }
        public List<CurrencyData> Rates { get; set; }
    }
}
