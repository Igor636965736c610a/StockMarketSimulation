using AutoMapper;
using Domain.AccountProperties.UserEntity;
using Domain.IRepo;
using Infrastructure.AutoMapper;
using Infrastructure.Repo;
using infrastructure.Services;
using Infrastructure.Services.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton(AutoMapperConfig.Initialize());
        services.AddHttpClient("CoinGecko", httpClient => 
                httpClient.BaseAddress = new Uri("https://api.coingecko.com/api/v3/"),
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json"));
        services.AddScoped<IGetCryptoCurrencyData, GetCryptoCurrencyData>();
        services.AddScoped<ICryptoTransactions, CryptoTransactions>();
        services.AddScoped<IUserRepo, UserRepo>();
        
        return services;
    }
}