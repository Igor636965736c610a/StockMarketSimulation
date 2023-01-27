using Domain.AccountProperties.UserEntity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.db;

public class StockMarketContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
    public StockMarketContext(DbContextOptions options) : base(options)
    {

    }
}