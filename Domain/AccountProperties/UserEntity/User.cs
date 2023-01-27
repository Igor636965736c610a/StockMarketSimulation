namespace Domain.AccountProperties.UserEntity;

public class User
{
    public User(string name, Wallet wallet)
    {
        Name = name;
        Wallet = wallet;
    }
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public Wallet Wallet { get; private set; }
    public CryptoWallet CryptoWallet { get; private set; } = new();
}