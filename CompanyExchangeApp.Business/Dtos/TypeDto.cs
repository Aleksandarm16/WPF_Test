namespace CompanyExchangeApp.Business.Dtos
{
    public class TypeDto : ICloneable
    {
        public long Id { get; set; }
        public string? Name { get; set; }

        public object Clone()
        {
            return new TypeDto
            {
                Id = this.Id,
                Name = this.Name,
            };
        }            
    }
}
