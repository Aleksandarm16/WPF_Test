using AutoMapper;
using CompanyExchangeApp.Business.Dtos;
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

        public async Task<IList<ExchangeDto>> GetExchangesAsync()
        {
            try
            {
                using (var dbContext = new DatabaseContext())
                {
                    dbContext.SetConnectionString("Data Source=" + _dbConnectionString);
                    dbContext.Database.EnsureCreated();
                    IList<Exchange> exchanges =await dbContext.Exchanges.ToListAsync();
                    IList<ExchangeDto> exchangesDto = AutoMapperConfig.Mapper.Map<IList<ExchangeDto>>(exchanges);
                    return exchangesDto;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<ExchangeDto>();
            }
        }

        public async Task<IList<SymbolDto>> GetAllSymbolsAsync(TypeDto? type = null, ExchangeDto? exchange = null)
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
                    IList<SymbolDto> symbolsDto = AutoMapperConfig.Mapper.Map<IList<SymbolDto>>(symbols);

                    return symbolsDto;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<SymbolDto>();
            }
        }

        public async Task <IList<TypeDto>> GetTypesAsync()
        {
            try
            {
                using (var dbContext = new DatabaseContext())
                {
                    dbContext.SetConnectionString("Data Source=" + _dbConnectionString);
                    dbContext.Database.EnsureCreated();
                    IList<Type> types = await dbContext.Types.ToListAsync();
                    IList<TypeDto> typesDto = AutoMapperConfig.Mapper.Map<IList<TypeDto>>(types);

                    return typesDto;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<TypeDto>();
            }
        }

        public void SetDbConnectionString(string connectionString)
        {
            _dbConnectionString = connectionString;
        }
    }
}
