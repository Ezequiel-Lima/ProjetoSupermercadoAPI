using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoSupermercado.Models;

namespace ProjetoSupermercado.Infrastructure.Entityconfiguration;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.ToTable("User", ApplicationDataContext.DEFAULT_SCHENA);

        entity.Property(e => e.Id).ValueGeneratedNever();

        entity.Property(e => e.Name)
            .IsRequired()
            .IsUnicode(false);

        entity.Property(e => e.Login)
            .IsRequired()
            .IsUnicode(false);

        entity.Property(e => e.Password)
            .IsRequired()
            .IsUnicode(false);
    }
}