using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MisGastos.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisGastos.Infrastructure.Context.Configurations
{
    public class GastoCreditoConfiguration : IEntityTypeConfiguration<GastoCredito>
    {
        public void Configure(EntityTypeBuilder<GastoCredito> builder)
        {
            builder.ToTable("GastoCredito");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.GastoId)
                   .HasColumnName("IdGasto");

        }
    }
}
