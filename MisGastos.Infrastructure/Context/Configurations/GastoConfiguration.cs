using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MisGastos.Core.Entities;

namespace MisGastos.Infrastructure.Context.Configurations
{
    public class GastoConfiguration : IEntityTypeConfiguration<Gasto>
    {
        public void Configure(EntityTypeBuilder<Gasto> builder)
        {
            builder.ToTable("Gasto");                

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Descripcion)
                .HasColumnName("Descripcion")
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.FrecuenciaId)
                .HasColumnName("IdFrecuencia");
            
            builder.Property(e => e.GastoTipoId)
                .HasColumnName("IdGastoTipo");
            
            builder.Property(e => e.MesId)
                .HasColumnName("IdMes");

            builder.HasMany(x => x.GastosCreditos).WithOne(b => b.Gasto).HasForeignKey(b => b.GastoId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(e => !e.Eliminado); 
           
        }

    }
}
