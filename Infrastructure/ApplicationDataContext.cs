using Microsoft.EntityFrameworkCore;
using ProjetoSupermercado.Infrastructure.Entityconfiguration;
using ProjetoSupermercado.Models;

namespace ProjetoSupermercado.Infrastructure;
public class ApplicationDataContext : DbContext
{
    public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
    {
    }

    public const string DEFAULT_SCHENA = "ProjetoEstoque";
    
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Sales> Sales { get; set; }
    public virtual DbSet<Stock> Stocks { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new SalesConfiguration());
        modelBuilder.ApplyConfiguration(new StockConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}