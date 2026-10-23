using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patrimonio.Models;

namespace Patrimonio.Context;

public class CadastroConfiguration : IEntityTypeConfiguration<Cadastro>
{
    public void Configure(EntityTypeBuilder<Cadastro> builder)
    {
        builder.ToTable("CADASTRO");

        builder.HasKey(e => new
        {
            e.EmpresaId, e.EmpresaFilialId, e.NumeroPatrimonial, e.TipoIncorporacaoId, e.Incorporacao
        });

        builder.Property(e => e.EmpresaId)
            .HasColumnName("EMPRESAID")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.EmpresaFilialId)
            .HasColumnName("EMPRESAFILIALID")
            .HasMaxLength(50);

        builder.Property(e => e.NumeroPatrimonial)
            .HasColumnName("NUMEROPATRIMONIAL")
            .HasMaxLength(20);

        builder.Property(e => e.TipoIncorporacaoId)
           .HasColumnName("TIPOINCORPORACAOID")
           .HasMaxLength(11);

         builder.Property(e => e.Incorporacao)
            .HasColumnName("INCORPORACAO")
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(e => e.CentroCustoId)
           .HasColumnName("CENTRODECUSTOID")
           .HasMaxLength(50);

         builder.Property(e => e.Descricao)
            .HasColumnName("DESCRICAORESUMIDA")
            .HasMaxLength(255)
            .IsRequired();




    }
}
