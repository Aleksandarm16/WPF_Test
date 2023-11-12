using AutoMapper;
using CompanyExchangeApp.Business.Dtos;
using CompanyExchangeApp.Business.Models;
using Type = CompanyExchangeApp.Business.Models.Type;

namespace CompanyExchangeApp.Business
{
    public static class AutoMapperConfig
    {
        private static IMapper _mapper;

        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    Configure();
                }
                return _mapper;
            }
        }

        public static void Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Symbol, SymbolDto>()
                   .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                   .ForMember(dest => dest.Exchange, opt => opt.MapFrom(src => src.Exchange));

                cfg.CreateMap<Exchange, ExchangeDto>();
                cfg.CreateMap<Type, TypeDto>();

                cfg.CreateMap<SymbolDto, Symbol>();
                cfg.CreateMap<ExchangeDto, Exchange>();
                cfg.CreateMap<TypeDto, Type>();
            });

            _mapper = config.CreateMapper();
        }
    }

}
