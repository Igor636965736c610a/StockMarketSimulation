using Domain.AccountProperties;

namespace Infrastructure.DTO;

public class SimpleCoinDTO
{
    public string Id { get; set; }
    public double CurrentPrice { get; set; }
    public Currency Currency { get; set; }
}