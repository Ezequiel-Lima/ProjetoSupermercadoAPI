using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoSupermercado.Models;

namespace ProjetoSupermercado.Infrastructure.Entityconfiguration;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entity)
    {
        entity.ToTable("Product", ApplicationDataContext.DEFAULT_SCHENA);

        entity.Property(e => e.Id).ValueGeneratedOnAdd();

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200)
            .IsUnicode(false);

        entity.Property(e => e.Price)
            .IsRequired()
            .IsUnicode(false);
    }
}