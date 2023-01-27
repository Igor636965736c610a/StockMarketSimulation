using Domain.AccountProperties;
using Domain.AccountProperties.UserEntity;
using Domain.IRepo;
using infrastructure.Services;

namespace Infrastructure.Services.Transactions;

public class CryptoTransactions : ICryptoTransactions
{
    private readonly IUserRepo _userRepo;
    private readonly IGetCryptoCurrencyData _getCryptoCurrencyData;
    public CryptoTransactions(IUserRepo userRepo, IGetCryptoCurrencyData getCryptoCurrencyData)
    {
        _userRepo = userRepo;
        _getCryptoCurrencyData = getCryptoCurrencyData;
    }
    public async Task BuyCryptoAsMoney(string cryptoCurrencyId, double moneyValue, Guid userId)
    {
        var user = await _userRepo.GetUserById(userId);
        if (user is null)
            throw new NullReferenceException($"User with {userId} does not exist");
        var crypto = await _getCryptoCurrencyData.GetSimpleCoinPrice(cryptoCurrencyId, Currency.USD);
        var cryptoValue = moneyValue / crypto.CurrentPrice;
        await ServiceWalletBuy(cryptoCurrencyId, moneyValue, cryptoValue, user);
        await _userRepo.UpdateUser(user);
    }
    public async Task BuyCryptoAsCryptoAmount(string cryptoCurrencyId, double cryptoValue, Guid userId)
    {
        var user = await _userRepo.GetUserById(userId);
        if (user is null)
            throw new NullReferenceException($"User with {userId} does not exist");
        var crypto = await _getCryptoCurrencyData.GetSimpleCoinPrice(cryptoCurrencyId, Currency.USD);
        var moneyValue = cryptoValue * crypto.CurrentPrice;
        await ServiceWalletBuy(cryptoCurrencyId, moneyValue, cryptoValue, user);
        await _userRepo.UpdateUser(user);
    }
    public async Task SellCryptoAsMoney(string cryptoCurrencyId, double moneyValue, Guid userId)
    {
        var user = await _userRepo.GetUserById(userId);
        if (user is null)
            throw new NullReferenceException($"User with {userId} does not exist");
        var crypto = await _getCryptoCurrencyData.GetSimpleCoinPrice(cryptoCurrencyId, Currency.USD);
        var cryptoValue = moneyValue * crypto.CurrentPrice;
        await ServiceWalletSell(cryptoCurrencyId, moneyValue, cryptoValue, user);
        await _userRepo.UpdateUser(user);
    }
    public async Task SellCryptoAsCryptoAmount(string cryptoCurrencyId, double cryptoValue, Guid userId)
    {
        var user = await _userRepo.GetUserById(userId);
        if (user is null)
            throw new NullReferenceException($"User with {userId} does not exist");
        var crypto = await _getCryptoCurrencyData.GetSimpleCoinPrice(cryptoCurrencyId, Currency.USD);
        var moneyValue = crypto.CurrentPrice * cryptoValue;
        await ServiceWalletSell(cryptoCurrencyId, moneyValue, cryptoValue, user);
        await _userRepo.UpdateUser(user);
    }
    private async static Task ServiceWalletBuy(string cryptoCurrencyId, double moneyValue, double cryptoValue, User user)
    {
        if (moneyValue > user.Wallet.Money)
            throw new InvalidOperationException("You don't have enough money");
        var coinPurchased = user.CryptoWallet.CryptoCurrency.FirstOrDefault(x => x.CoinId == cryptoCurrencyId);
        if (coinPurchased is null)
            user.CryptoWallet.CryptoCurrency.Add(new BaseCrypto(cryptoCurrencyId, cryptoValue));
        else
            coinPurchased.Amount += cryptoValue;
        user.Wallet.Money -= moneyValue;
    }
    private async static Task ServiceWalletSell(string cryptoCurrencyId, double moneyValue, double cryptoValue, User user)
    {
        var cryptoToSell = user.CryptoWallet.CryptoCurrency.FirstOrDefault(x => x.CoinId == cryptoCurrencyId);
        if (cryptoToSell is null && cryptoValue > cryptoToSell.Amount)
            throw new InvalidOperationException("You can't sell this cryptoCurrency");
        user.Wallet.Money += moneyValue;
        cryptoToSell.Amount -= moneyValue;
        if (cryptoToSell.Amount == 0)
            user.CryptoWallet.CryptoCurrency.Remove(cryptoToSell);
    }
}
