using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patrimonio.Models;
using Patrimonio.Models.ValueObjects;

namespace Patrimonio.Context;

public sealed class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
       
        builder.ToTable("EMPRESAS");

        builder.HasKey(e => e.EmpresaId);

        builder.Property(e => e.EmpresaId)
            .HasColumnName("EMPRESAID")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.RazaoSocial)
            .HasColumnName("RAZAOSOCIAL")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.CGC)
           .HasColumnName("CGC")
           .HasConversion(
                cgc => cgc.Numero,
                valor => new Cgc(valor))
           .HasMaxLength(50);

        builder.Property(e => e.Endereco)
           .HasColumnName("ENDERECO")
           .HasMaxLength(255);

        builder.Property(e => e.Telefone)
            .HasColumnName("TELEFONE")
            .HasMaxLength(255);

        builder.Property(e => e.Ramal)
            .HasColumnName("RAMAL")
            .HasMaxLength(255);

        builder.Property(e => e.FAX)
            .HasColumnName("FAX")
            .HasMaxLength(255);

        builder.Property(e => e.Cidade)
            .HasColumnName("CIDADE")
            .HasMaxLength(50);

        builder.Property(e => e.Estado)
            .HasColumnName("ESTADO")
            .HasMaxLength(50);

        builder.Property(e => e.CEP)
            .HasColumnName("CEP")
            .HasMaxLength(50);

        builder.Property(e => e.PlacaCorporea)
            .HasColumnName("PLACACORPOREA");

        builder.Property(e => e.PlacaNaoCorporea)
            .HasColumnName("PLACANAOCORPOREA");

        builder.Property(e => e.CalculoDepreciacao)
            .HasColumnName("CALCULADEPRECIACAO")
            .HasMaxLength(1);

        builder.Property(e => e.CalculoDepreciacaoDollar)
            .HasColumnName("CALCULADEPRECIACAODOLLAR")
            .HasMaxLength(1);

        builder.Property(e => e.DepreciaMesAquisicao)
            .HasColumnName("DEPRECIAMESAQUISICAO")
            .HasMaxLength(1);

        builder.Property(e => e.DepreciaMesAquisicaoDollar)
            .HasColumnName("DEPRECIAMESAQUISICAODOLLAR")
            .HasMaxLength(1);

        builder.Property(e => e.CompetenciaInicial)
            .HasColumnName("COMPETENCIAINICIAL");

        builder.Property(e => e.CompetenciaFinal)
            .HasColumnName("COMPETENCIAFINAL");

        builder.Property(e => e.TipoCalculo)
            .HasColumnName("TIPO_CALCULO")
            .HasMaxLength(10);

        builder.Property(e => e.FilialCentralizadora)
            .HasColumnName("FILIAL_CENTRALIZADORA")
            .HasMaxLength(5);
    }
}
