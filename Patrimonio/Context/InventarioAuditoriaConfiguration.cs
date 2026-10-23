using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patrimonio.Models;

namespace Patrimonio.Context;

public class InventarioAuditoriaConfiguration : IEntityTypeConfiguration<InventarioAuditoria>
{
    public void Configure(EntityTypeBuilder<InventarioAuditoria> builder)
    {

    builder.ToTable("INVENTARIO_AUDITORIA");

        builder.HasKey(e => new
        {
            e.InventarioId,
            e.EmpresaId,
            e.NumeroPatrimonial,
            e.TipoIncorporacaoId,
            e.Incorporacao
        });

        builder.Property(e => e.InventarioId)
            .HasColumnName("INVENTARIOID")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(e => e.EmpresaId)
            .HasColumnName("EMPRESAID")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.EmpresaFilialId)
            .HasColumnName("EMPRESAFILIALID")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.CentroCustoId)
            .HasColumnName("CENTRODECUSTOID")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.NumeroPatrimonial)
            .HasColumnName("NUMEROPATRIMONIAL")
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(e => e.TipoIncorporacaoId)
            .HasColumnName("TIPOINCORPORACAO")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(e => e.Incorporacao)
            .HasColumnName("INCORPORACAO")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(e => e.DataAquisicao)
            .HasColumnName("DATAAQUISICAO")
            .HasColumnType("DATE");

        builder.Property(e => e.DescricaoResumida)
            .HasColumnName("DESCRICAORESUMIDA")
            .HasMaxLength(255);
            
        builder.Property(e => e.ContaId)
            .HasColumnName("CONTAID")            
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.ValorAtualizado)
            .HasColumnName("VALORATUALIZADO")
            .HasPrecision(38,2);

        builder.Property(e => e.DepreciacaoAcumulada)
            .HasColumnName("DEPRECIACAOACUMULADA")
            .HasPrecision(38, 2);

        builder.Property(e => e.ValorResidual)
            .HasColumnName("VALORRESIDUAL")
            .HasPrecision(38,2);

        builder.Property(e => e.ProdutoId)
            .HasColumnName("PRODUTOID")
            .HasMaxLength(12);

        builder.Property(e => e.Achoubem)
            .HasColumnName("ACHOU_BEM")
            .HasMaxLength(1);

        builder.Property(e => e.NovaDescricao)
            .HasColumnName("NOVADESCRICAO")
            .HasMaxLength(255);

        builder.Property(e => e.NovoCentroCusto)
            .HasColumnName("NOVO_CCUSTO")
            .HasMaxLength(50);
        
        builder.Property(e => e.NovaPlaqueta)
            .HasColumnName("NOVA_PLAQUETA")
            .HasMaxLength(20);

        builder.Property(e => e.Validacao)
            .HasColumnName("VALIDACAO")
            .HasMaxLength(1);

        builder.Property(e => e.UsuarioDefinicao)
            .HasColumnName("USUARIO_DEFINICAO")
            .HasMaxLength(100);
        
        builder.Property(e => e.DataDefinicao)  
            .HasColumnName("DATA_DEFINICAO")
            .HasColumnType("DATE");
      
        builder.Property(e => e.Observacao)
            .HasColumnName("OBSERVACAO")
            .HasMaxLength(2000);

        builder.Property(e => e.FotoPlaqueta) //////
            .HasColumnName("FOTO_PLAQUETA")
            .HasColumnType("BLOB");

        builder.Property(e => e.FotoEquipamento)
            .HasColumnName("FOTO_EQUIPAMENTO")
            .HasColumnType("BLOB");
    }
}
