namespace CompanyExchangeApp.Business.Dtos
{
    public class ExchangeDto : ICloneable
    {
        public long Id { get; set; }

        public string? Name { get; set; } = null!;

        public object Clone()
        {
            return new ExchangeDto
            {
                Id = this.Id,
                Name = this.Name,
            };
        }
    }
}
