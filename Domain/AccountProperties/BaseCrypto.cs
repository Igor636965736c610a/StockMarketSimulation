namespace Domain.AccountProperties;

public class BaseCrypto
{
    public BaseCrypto(string coinId)
    {
        CoinId = coinId;
    }
    public BaseCrypto(string coinId, double amount)
    {
        CoinId = coinId;
        Amount = amount;
    }
    public string CoinId { get; private set; }
    public double Amount { get; set; } = 0;
}