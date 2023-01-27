using AutoMapper;
using Domain.AccountProperties;
using Domain.CryptoCurrencyModels;
using Infrastructure.DTO;
using Newtonsoft.Json;

namespace infrastructure.Services;

public class GetCryptoCurrencyData : IGetCryptoCurrencyData
{
   private readonly IHttpClientFactory _httpClientFactory;
   private readonly IMapper _mapper;
   public GetCryptoCurrencyData(IHttpClientFactory httpClientFactory, IMapper mapper)
   {
      _httpClientFactory = httpClientFactory;
      _mapper = mapper;
   }

   public async Task<List<Coin>> GetCoins(Currency currency) // currency like usd
   {
      var currencyUri = nameof(currency).ToLower();
      var httpClient = _httpClientFactory.CreateClient("CoinGecko");
      var httpResponseMessage = await httpClient.GetAsync($"/coins/markets?vs_currency={currencyUri}&order=market_cap_desc&per_page=100&page=1&sparkline=false");
      var coinsDtoJSON = await httpResponseMessage.Content.ReadAsStringAsync();
      var coinsDto = JsonConvert.DeserializeObject<List<CoinDTO>>(coinsDtoJSON);
      return _mapper.Map<List<Coin>>((coinsDto, currency));
   }

   public async Task<SimpleCoinDTO> GetSimpleCoinPrice(string id, Currency currency)
   { 
      var currencyUri = nameof(currency).ToLower();
      var httpClient = _httpClientFactory.CreateClient("CoinGecko");
      var httpResponseMessage = await httpClient.GetAsync($"https://api.coingecko.com/api/v3/simple/price?ids={id}&vs_currencies={currencyUri}");
      var coinDtoJSON = await httpResponseMessage.Content.ReadAsStringAsync();
      var coinDto = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, double>>>(coinDtoJSON);
      return new SimpleCoinDTO()
      {
         Id = id,
         Currency = currency,
         CurrentPrice = coinDto[id][currencyUri],
      };
   }
}