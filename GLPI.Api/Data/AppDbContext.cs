using System.Reflection;
using GLPI.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GLPI.Api.Data;

/* 
    AppDbContext eh focado apenas em criar as tabelas e o schema principal do nosso sistema.
    Porem, precisamos nos ater com o tipo de DBContext.
*/
public class AppDbContext : DbContext
{
    public DbSet<Ticket> Tickets {get;set;} = null!;
    public DbSet<Category> Categories {get;set;} = null!;
    public DbSet<Status> Status {get;set;} = null!;


    /* 
        Funcao focada em quando criar o banco pela primeira vez, vamos mapear manualmente cada variavel para cada tipo de coluna correta!
        Obviamente, ele ira funcionar toda vez que o banco for criado. 
        Entao, caso queremos dar algum mudanca de tipo de coluna, vamos utilizar o migration
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        // faz varias configurations inves de uma por uma, ou seja, os nossos mappings vao ser acessados em varios files inves de eu declarar um por um.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 
    }
}
