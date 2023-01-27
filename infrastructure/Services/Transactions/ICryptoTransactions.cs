namespace Infrastructure.Services.Transactions;

public interface ICryptoTransactions
{
    public Task BuyCryptoAsMoney(string cryptoCurrencyId, double moneyValue, Guid userId);
    public Task BuyCryptoAsCryptoAmount(string cryptoCurrencyId, double cryptoValue, Guid userId);
    public Task SellCryptoAsMoney(string cryptoCurrencyId, double moneyValue, Guid userId);
    public Task SellCryptoAsCryptoAmount(string cryptoCurrencyId, double cryptoValue, Guid userId);
}