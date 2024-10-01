using GLPI.Api.Data;
using GLPI.Api.Handlers;
using GLPI.Core.Handlers;
using GLPI.Core.Models;
using GLPI.Core.Requests.Categories;
using GLPI.Core.Responses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var cnnStr = builder
    .Configuration // Ve no configs do appsettings.json, essencial para colocar origens de certos arquivos e bancos.
    .GetConnectionString("DefaultConnection") ?? string.Empty; // Busca no arquivo appsettings o elemento em string, nesse caso o defaultconnection.

// Adiciona um contexto de banco dados(Dependencia), essencial para o crud e operacoes
builder.Services.AddDbContext<AppDbContext>(x => {
    x.UseSqlServer(cnnStr); // Especifica que o AppDbContext deve usar um Sql Server com o cnnstr como o path
});

// Cria as abas de API para cada rota
builder.Services.AddEndpointsApiExplorer();

// Cria o Swagger
builder.Services.AddSwaggerGen(x => { x.CustomSchemaIds(n => n.FullName); });

/*
    Add Transient -> Sempre vai criar uma nova instancia do HANDLER
    Add Scoped -> So vai existir 1 instancia por requisicao 
    Add Singleton -> 1 e so 1 instancia por aplicacao (o handler existira na memoria da aplicacao somente sendo ela.)
*/
builder.Services.AddTransient<ICategoryHandler,CategoryHandler>();



var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.MapPost(
        pattern: "/v1/categories",
        handler: async (
                CreateCategoryRequest request,
                ICategoryHandler handler)
            => await handler.CreateAsync(request))
    .WithName("Categories: Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response<Category?>>();


app.MapPut(
        pattern: "/v1/categories/{id}",
        handler: async (
                long id,
                UpdateCategoryRequest request,
                ICategoryHandler handler)
            => 
            {
                request.Id = id; 
                return await handler.UpdateAsync(request);
            })
    .WithName("Categories: Update")
    .WithSummary("Atualiza uma nova categoria")
    .Produces<Response<Category?>>();

app.MapDelete(
        pattern: "/v1/categories/{id}",
        handler: async (
                long id,
                ICategoryHandler handler)
            => 
            {
                var request = new DeleteCategoryRequest
                {
                    Id = id
                };
                return await handler.DeleteAsync(request);
            })
    .WithName("Categories: Delete")
    .WithSummary("Exclui uma categoria")
    .Produces<Response<Category?>>();


app.MapGet(
        pattern: "/v1/categories/{id}",
        handler: async (
                long id,
                ICategoryHandler handler)
            => 
            {
                var request = new GetByIdCategoryRequest
                {
                    Id = id
                };
                return await handler.GetByIdAsync(request);
            })
    .WithName("Categories: Get by Id")
    .WithSummary("Recupera um category via ID")
    .Produces<Response<Category?>>();


app.MapGet(
        pattern: "/v1/categories",
        handler:async (
                ICategoryHandler handler)
            => 
            {
                var request = new GetAllCategoryRequest();

                return await handler.GetAllAsync(request);
            })
    .WithName("Categories: Get all Categories")
    .WithSummary("Pega todas as categorias")
    .Produces<PagedResponse<List<Category?>>>();

app.Run();