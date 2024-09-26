using GLPI.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GLPI.Api.Data.Mappings;

/*
    IEntityTypeConfiguration, vamos herdar essa classe do entityframework focada apenas em mapear tipos especificos no nosso banco
    Como estamos usando SqlServer, vamos utilizar tipos que sao aceitos no SqlServer!
    Ele precisa de um Tipo, esse tipo eh o nosso model category.
*/
public class CategoryMapping : IEntityTypeConfiguration<Category>
{
    /*
        Temos a classe EntityTypeBuilder, ela tambem precisa de um tipo.
        Sendo o nosso caso o category, essa classe vai ter metodos que podemos mapear cada coluna do banco.
    */
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category"); // Nome da tabela eh "Category"

        builder.HasKey(x => x.Id); // Primary Key eh o Id, utilizando um snippet

        builder.Property(x => x.Name)    // Pegamos a coluna Name.
            .IsRequired(true)            // Define como requirida
            .HasColumnType("NVARCHAR")   // Define o tipo da coluna como varchar
            .HasMaxLength(80);           // Define o tamanho maximo da coluna

        builder.Property(x => x.Color)
        .IsRequired(true)
        .HasColumnType("NVARCHAR")
        .HasMaxLength(80);
    }
}