using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patrimonio.Models;

namespace Patrimonio.Context;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("PRODUTO");

        builder.HasKey(e => e.ProdutoId);
        builder.Property(e => e.ProdutoId)
            .HasColumnName("PRODUTOID")
            .HasMaxLength(12);

        builder.Property(e => e.Descricao)
            .HasColumnName("DESCRICAO")
            .HasMaxLength(200);
    }        
}
