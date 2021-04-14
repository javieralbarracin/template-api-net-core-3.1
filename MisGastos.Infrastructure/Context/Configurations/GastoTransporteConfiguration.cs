using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MisGastos.Core.Entities;

namespace MisGastos.Infrastructure.Context.Configurations
{
    public class GastoTransporteConfiguration : IEntityTypeConfiguration<GastoTransporte>
    {
        public void Configure(EntityTypeBuilder<GastoTransporte> builder)
        {
            builder.ToTable("GastoTransporte");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Descripcion)
                .HasColumnName("Descripcion")
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);                

        }

    }
}

