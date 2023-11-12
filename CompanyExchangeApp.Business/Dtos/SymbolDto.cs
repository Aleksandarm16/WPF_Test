using System.ComponentModel;

namespace CompanyExchangeApp.Business.Dtos
{
    public class SymbolDto : ICloneable
    {

        private int? _id { get; set; }
        public int? Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string? _name { get; set; }
        public string? Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string? _ticker { get; set; }
        public string? Ticker
        {
            get { return _ticker; }
            set { _ticker = value; }
        }
        private string? _isin { get; set; }
        public string? Isin
        {
            get { return _isin; }
            set { _isin = value; }
        }
        private string? _currencyCode { get; set; }
        public string? CurrencyCode
        {
            get { return _currencyCode; }
            set { _currencyCode = value; }
        }
        private DateOnly _dateAdded { get; set; }
        public DateOnly DateAdded
        {
            get { return _dateAdded; }
            set { _dateAdded = value; }
        }
        private double? _price { get; set; }
        public double? Price
        {
            get { return _price; }
            set { _price = value; }
        }
        private DateOnly _priceDate { get; set; }
        public DateOnly PriceDate
        {
            get { return _priceDate; }
            set { _priceDate = value; }
        }
        private TypeDto? _type { get; set; }
        public TypeDto? Type
        {
            get { return _type; }
            set { _type = value; }
        }
        private ExchangeDto? _exchange { get; set; }
        public ExchangeDto? Exchange
        {
            get { return _exchange; }
            set { _exchange = value; }
        }

        public object Clone()
        {
            return new SymbolDto
            {
                Id = this.Id,
                Name = this.Name,
                Ticker = this.Ticker,
                Isin = this.Isin,
                CurrencyCode = this.CurrencyCode,
                DateAdded = this.DateAdded,
                Price = this.Price,
                PriceDate = this.PriceDate,
                Type = this.Type?.Clone() as TypeDto, 
                Exchange = this.Exchange?.Clone() as ExchangeDto 
            };
        }

    }
}
