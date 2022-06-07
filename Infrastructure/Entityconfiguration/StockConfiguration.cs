using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoSupermercado.Models;

namespace ProjetoSupermercado.Infrastructure.Entityconfiguration;
public class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> entity)
    {
        entity.ToTable("Stock", ApplicationDataContext.DEFAULT_SCHENA);

        entity.Property(e => e.Id).ValueGeneratedOnAdd();

        entity.Property(e => e.ProductQuantity)
            .IsRequired()
            .IsUnicode(false);

        entity.Property(e => e.ProductName)
            .IsRequired()
            .IsUnicode(false);

        entity.HasOne(e => e.Product)
            .WithMany()
            .HasForeignKey(e => e.ProductId);
    }
}