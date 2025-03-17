namespace CurrencyConverterAPI.Models
{
    public class CurrencyRequest
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal Amount { get; set; }
        public string Provider { get; set; } // Ensure this property is defined
    }
}
