using AutoMapper;
using Domain.AccountProperties;
using Domain.CryptoCurrencyModels;
using Infrastructure.DTO;

namespace Infrastructure.AutoMapper;

public static class AutoMapperConfig
{
    public static IMapper Initialize()
        => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<(CoinDTO coinDto, Currency currency), Coin>()
                    .ForMember(x => x.Currency,
                        opt => opt.MapFrom(s => s.currency))
                    .ForMember(c => c.Id,
                        opt => opt.MapFrom(s => s.coinDto.Id))
                    .ForMember(c => c.MarketCap,
                        opt => opt.MapFrom(s => s.coinDto.MarketCap))
                    .ForMember(c => c.Name,
                        opt => opt.MapFrom(s => s.coinDto.Name))
                    .ForMember(c => c.Symbol,
                        opt => opt.MapFrom(s => s.coinDto.Symbol))
                    .ForMember(c => c.CurrentPrice,
                        opt => opt.MapFrom(s => s.coinDto.CurrentPrice))
                    .ForMember(c => c.MarketCapRank,
                        opt => opt.MapFrom(s => s.coinDto.MarketCapRank));
            })
            .CreateMapper();
}