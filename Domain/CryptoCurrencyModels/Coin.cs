using Domain.AccountProperties;

namespace Domain.CryptoCurrencyModels;

public class Coin
{
    public string Id { get; set; }
    public string Symbol { get; set; }
    public string Name { get; set; }
    public double CurrentPrice { get; set; }
    public double MarketCap { get; set; }
    public int MarketCapRank { get; set; }
    public Currency Currency { get; set; }
}