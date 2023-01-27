using Domain.AccountProperties;
using Domain.CryptoCurrencyModels;
using Infrastructure.DTO;

namespace infrastructure.Services;

public interface IGetCryptoCurrencyData
{
    public Task<List<Coin>> GetCoins(Currency currency);
    public Task<SimpleCoinDTO> GetSimpleCoinPrice(string id, Currency currency);
}