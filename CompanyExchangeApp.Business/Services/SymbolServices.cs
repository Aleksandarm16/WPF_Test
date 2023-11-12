using CompanyExchangeApp.Business.Dtos;
using CompanyExchangeApp.Business.Interface;
using CompanyExchangeApp.Business.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task SaveSymbolAsync(SymbolDto symbolDto)
        {
            try
            {
                using (var dbContext = new DatabaseContext())
                {
                    dbContext.SetConnectionString("Data Source=" + _dbConnectionString);
                    // Ensure the database is created
                    dbContext.Database.EnsureCreated();

                    // Map the SymbolDto to Symbol entity
                    Symbol symbol = AutoMapperConfig.Mapper.Map<Symbol>(symbolDto);

                    // Check if the associated Type and Exchange records exist
                    Type existingType = await dbContext.Types.FindAsync(symbol.TypeId);
                    Exchange existingExchange = await dbContext.Exchanges.FindAsync(symbol.ExchangeId);

                    if (existingType == null || existingExchange == null)
                    {
                        // Type or Exchange record doesn't exist, handle accordingly (throw an exception, log, etc.)
                        Console.WriteLine("Type or Exchange record not found. Symbol not saved.");
                        return;
                    }

                    // Set navigation properties
                    symbol.Type = existingType;
                    symbol.Exchange = existingExchange;
                    // Check if the symbol already exists in the database
                    Symbol existingSymbol = await dbContext.Symbols.FindAsync(symbol.Id);

                    if (existingSymbol != null)
                    {
                       // Symbol exists, update its properties
                        dbContext.Entry(existingSymbol).CurrentValues.SetValues(symbol);
                   }
                    else
                   {
                        // Symbol doesn't exist, add it to the database
                        dbContext.Symbols.Add(symbol);
                   }

                    // Set the state of related entities to Unchanged to prevent them from being added or updated
                    dbContext.Entry(existingType).State = EntityState.Unchanged;
                    dbContext.Entry(existingExchange).State = EntityState.Unchanged;

                    // Save changes to the database
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving symbol: {ex.Message}");
            }
        }

        public async Task DeleteSymbolAsync(SymbolDto symbolDto)
        {
            try
            {
                if (symbolDto == null || symbolDto.Id == null)
                {
                    // Handle the case where the symbol or its ID is null
                    return;
                }

                using (var dbContext = new DatabaseContext())
                {
                    dbContext.SetConnectionString("Data Source=" + _dbConnectionString);
                    // Ensure the database is created
                    dbContext.Database.EnsureCreated();

                    // Find the symbol in the database by its ID
                    var symbolToDelete = await dbContext.Symbols.FindAsync(symbolDto.Id);

                    if (symbolToDelete != null)
                    {
                        // Remove the symbol from the database
                        dbContext.Symbols.Remove(symbolToDelete);
                        await dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        // Handle the case where the symbol with the specified ID is not found
                        Console.WriteLine($"Error deleting symbol specified ID is not found");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions accordingly (logging, etc.)
                Console.WriteLine($"Error deleting symbol: {ex.Message}");
            }
        }

    }
}
