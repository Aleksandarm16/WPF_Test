using CompanyExchangeApp.Business.Dtos;

namespace CompanyExchangeApp.Business.Interface
{
    public interface ISymbolService
    {
        public Task<IList<SymbolDto>> GetAllSymbolsAsync(TypeDto? type = null, ExchangeDto? exchange = null);
        public Task<IList<TypeDto>> GetTypesAsync();
        public Task<IList<ExchangeDto>> GetExchangesAsync();
        public void SetDbConnectionString(string connectionString);
    }
}
