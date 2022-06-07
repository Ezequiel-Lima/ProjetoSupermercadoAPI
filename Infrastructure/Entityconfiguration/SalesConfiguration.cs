using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoSupermercado.Models;

namespace ProjetoSupermercado.Infrastructure.Entityconfiguration;
public class SalesConfiguration : IEntityTypeConfiguration<Sales>
{
    public void Configure(EntityTypeBuilder<Sales> entity)
    {
        entity.ToTable("Sales", ApplicationDataContext.DEFAULT_SCHENA);

        entity.Property(e => e.Id).ValueGeneratedOnAdd();

        entity.Property(e => e.ProductQuantitySales)
            .IsRequired()
            .IsUnicode(false);

        entity.Property(e => e.ProductName)
            .IsRequired()
            .IsUnicode(false);

        entity.Property(e => e.SaleDate).HasColumnType("datetime");

        entity.HasOne(e => e.Product)
            .WithMany()
            .HasForeignKey(e => e.ProductId);
    }
}