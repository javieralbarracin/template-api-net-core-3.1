using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MisGastos.Core.Entities;

namespace MisGastos.Infrastructure.Context.Configurations
{
    public class MesConfiguration : IEntityTypeConfiguration<Mes>
    {
        public void Configure(EntityTypeBuilder<Mes> builder)
        {
            builder.ToTable("Mes");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Descripcion)
                .HasColumnName("Descripcion")
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}

