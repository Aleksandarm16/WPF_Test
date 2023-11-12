namespace CompanyExchangeApp.Business.Dtos
{
    public class SymbolDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Ticker { get; set; }
        public string? Isin { get; set; }
        public string? CurrencyCode { get; set; }
        public DateTime DateAdded { get; set; }
        public double? Price { get; set; }
        public DateTime PriceDate { get; set; }
        public TypeDto? Type { get; set; }
        public ExchangeDto? Exchange { get; set; }
    }
}
