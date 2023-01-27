namespace Domain.AccountProperties;

public class Wallet
{
    public Wallet(double money)
    {
        Money = money;
    }
    public double Money { get; set; }
    public Currency Currency { get; private set; } = Currency.USD;
}