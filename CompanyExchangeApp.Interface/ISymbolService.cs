using CompanyExchangeApp.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyExchangeApp.Interface
{
    public interface ISymbolService
    {

        IList<Symbol> GetSymbols();
    }
}
