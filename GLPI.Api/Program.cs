using GLPI.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var cnnStr = builder
    .Configuration // Ve no configs do appsettings.json, essencial para colocar origens de certos arquivos e bancos.
    .GetConnectionString("DefaultConnection") ?? string.Empty; // Busca no arquivo appsettings o elemento em string, nesse caso o defaultconnection.

// Adiciona um contexto de banco dados(Dependencia), essencial para o crud e operacoes
builder.Services.AddDbContext<AppDbContext>(x => {
    x.UseSqlServer(cnnStr); // Especifica que o AppDbContext deve usar um Sql Server com o cnnstr como o path
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.MapGet("/", () => new Response {
    Id = 2,
    Text = "Hello world" 
}).WithSummary("Cria uma nova transacao")
.Produces<Response>();

app.Run();



class Request()
{
    public int MyProperty { get; set; }
}

class Response()
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
}
