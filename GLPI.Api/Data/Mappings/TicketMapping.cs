using GLPI.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GLPI.Api.Data.Mappings;

/*
    IEntityTypeConfiguration, vamos herdar essa classe do entityframework focada apenas em mapear tipos especificos no nosso banco
    Como estamos usando SqlServer, vamos utilizar tipos que sao aceitos no SqlServer!
    Ele precisa de um Tipo, esse tipo eh o nosso model ticket.
*/
public class TicketMapping : IEntityTypeConfiguration<Ticket>
{
    /*
        Temos a classe EntityTypeBuilder, ela tambem precisa de um tipo.
        Sendo o nosso caso o ticket, essa classe vai ter metodos que podemos mapear cada coluna do banco.
    */
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Ticket"); // Nome da tabela eh "Category"

        builder.HasKey(x => x.Id); // Primary Key eh o Id, utilizando um snippet

        builder.Property(x => x.Title)    // Pegamos a coluna Name.
            .IsRequired(true)            // Define como requirida
            .HasColumnType("NVARCHAR")   // Define o tipo da coluna como varchar
            .HasMaxLength(80);           // Define o tamanho maximo da coluna

        builder.Property(x => x.CategoryId)
            .IsRequired(true)
            .HasColumnType("BIGINT") 
            .HasMaxLength(80);

        builder.Property(x => x.StatusId)
            .IsRequired(true)
            .HasColumnType("BIGINT")
            .HasMaxLength(80);

        builder.Property(x => x.CreatedAt)
            .IsRequired(true);

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);

        builder.HasOne(x => x.Category)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

        builder.HasOne(x => x.Status)
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
    }
}
