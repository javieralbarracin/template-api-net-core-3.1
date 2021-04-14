using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MisGastos.Core.Entities;

namespace MisGastos.Infrastructure.Context.Configurations
{
    public class GastoTipoDetalleConfiguration : IEntityTypeConfiguration<GastoTipoDetalle>
    {
        public void Configure(EntityTypeBuilder<GastoTipoDetalle> builder)
        {
            builder.ToTable("GastoTipoDetalle");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Descripcion)
                .HasColumnName("Descripcion")
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.GastoTipoId)
                .HasColumnName("IdGastoTipo");
            
         //   builder.HasQueryFilter(e => !e.Eliminado);             
        }
    }
}
