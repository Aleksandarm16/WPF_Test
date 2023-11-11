using CompanyExchangeApp.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = CompanyExchangeApp.Business.Models.Type;

namespace CompanyExchangeApp.Business.Interface
{
    public interface ISymbolService
    {
        public Task<IList<Symbol>> GetAllSymbolsAsync(Type? type = null, Exchange? exchange = null);
        public Task<IList<Type>> GetTypesAsync();
        public Task<IList<Exchange>> GetExchangesAsync();
        public void SetDbConnectionString(string connectionString);
    }
}
