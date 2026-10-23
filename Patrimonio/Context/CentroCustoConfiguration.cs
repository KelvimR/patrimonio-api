using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patrimonio.Models;

namespace Patrimonio.Context;

public class CentroCustoConfiguration : IEntityTypeConfiguration<CentroCusto>
{
    public void Configure(EntityTypeBuilder<CentroCusto> builder)
    {

        builder.ToTable("CENTRODECUSTO");

        builder.HasKey(e => new
        {
            e.EmpresaId,
            e.EmpresaFilialId,
            e.CentroCustoId,
            e.Descricao
        });

        builder.Property(e => e.CentroCustoId)
            .HasColumnName("CENTRODECUSTOID")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.EmpresaId)
            .HasColumnName("EMPRESAID")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.EmpresaFilialId)
            .HasColumnName("EMPRESAFILIALID")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.Descricao)
            .HasColumnName("DESCRICAO")
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(e => e.Ativo)
            .HasColumnName("ATIVO")
            .HasMaxLength(1)
            .IsRequired();
    }
}
