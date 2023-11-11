using CompanyExchangeApp.Business.Interface;
using CompanyExchangeApp.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = CompanyExchangeApp.Business.Models.Type;

namespace CompanyExchangeApp.Business.Services
{
    public class SymbolServices: ISymbolService
    {
        private string? _dbConnectionString;

        public async Task<IList<Exchange>> GetExchangesAsync()
        {
            try
            {
                using (var dbContext = new DatabaseContext())
                {
                    dbContext.SetConnectionString("Data Source=" + _dbConnectionString);
                    dbContext.Database.EnsureCreated();
                    IList<Exchange> exchanges =await dbContext.Exchanges.ToListAsync();
                    return exchanges;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Exchange>();
            }
        }

        public async Task<IList<Symbol>> GetAllSymbolsAsync(Type? type = null, Exchange? exchange = null)
        {
            try
            {
                using (var dbContext = new DatabaseContext())
                {
                    dbContext.SetConnectionString("Data Source=" + _dbConnectionString);
                    // Ensure the database is created
                    dbContext.Database.EnsureCreated();

                    IQueryable<Symbol> query = dbContext.Symbols
                             .Include(s => s.Exchange)
                             .Include(s => s.Type);

                    if (!string.IsNullOrEmpty(type?.Name) && type.Name != "ALL")
                    {
                        query = query.Where(s => s.Type.Name == type.Name);
                    }

                    if (!string.IsNullOrEmpty(exchange?.Name) && exchange.Name != "ALL")
                    {
                        query = query.Where(s => s.Exchange.Name == exchange.Name);
                    }

                    IList<Symbol> symbols = await query.ToListAsync();

                    return symbols;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Symbol>();
            }
        }

        public async Task <IList<Type>> GetTypesAsync()
        {
            try
            {
                using (var dbContext = new DatabaseContext())
                {
                    dbContext.SetConnectionString("Data Source=" + _dbConnectionString);
                    dbContext.Database.EnsureCreated();
                    IList<Type> types = await dbContext.Types.ToListAsync();
                    return types;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Type>();
            }
        }

        public void SetDbConnectionString(string connectionString)
        {
            _dbConnectionString = connectionString;
        }
    }
}
