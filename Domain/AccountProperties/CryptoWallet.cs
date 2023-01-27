
namespace Domain.AccountProperties;

public class CryptoWallet
{
    public List<BaseCrypto> CryptoCurrency { get; set; } = new();
    public int CountOfCryptoCurrenciesHeld => CryptoCurrency.Count;
}