using GLPI.Api.Data;
using GLPI.Core.Handlers;
using GLPI.Core.Models;
using GLPI.Core.Requests.Categories;
using GLPI.Core.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GLPI.Api.Handlers;
public class CategoryHandler(AppDbContext context) : ICategoryHandler
{

    public async Task<Response<Category>> CreateAsync(CreateCategoryRequest request)
    {
        try
        {
            var categories = await context.Categories.FirstOrDefaultAsync(x => x.Name == request.Name || x.Color == request.Color);

            if (categories is not null)
                return new Response<Category>(null, 404, "Categoria nao encontrada");

            var category = new Category
            {
                Name = request.Name,
                Color = request.Color
            };

            await context.AddAsync(category);
            await context.SaveChangesAsync();
            return new Response<Category>(category, 201, "Categoria criada com sucesso");
        }
        catch (Exception ex)
        {
            // Serilog verificar para estudo futuros.
            Console.WriteLine(ex);
            throw new Exception("Falha ao criar a categoria");
        }
    }

    public async Task<Response<Category>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (category is null)
                return new Response<Category>(null, 404, "Categoria nao encontrada");

            category.Name = request.Name;
            category.Color = request.Color;

            context.Categories.Update(category);
            await context.SaveChangesAsync();

            return new Response<Category>(category, message: "Categoria atualizada com sucesso");
        }
        catch
        {
            return new Response<Category>(null, 500, "Nao foi possivel excluir a categoria");
        }
    }

    public async Task<Response<Category>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (category is null)
                return new Response<Category>(null, 404, "Categoria nao encontrada");

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return new Response<Category>(category, message: "Categoria excluida com sucesso!");
        }
        catch
        {
            return new Response<Category>(null, 500, message: "Nao foi possivel excluir a categoria");
        }
    }

    public async Task<Response<Category>> GetByIdAsync(GetByIdCategoryRequest request)
    {
        // AsNoTracking -> vai garantir que nao vamos fazer modificacoes na tabela, apenas lendo as linhas.
        try
        {
            var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);

            return (category is null)
                ? new Response<Category>(null, 404, "Categoria nao encontrada")
                : new Response<Category>(category);
        }
        catch
        {
            return new Response<Category>(null, 500, "Nao foi possivel recuperar a categoria");
        }
    }

    public async Task<Response<List<Category>>> GetAllAsync(GetAllCategoryRequest request)
    {
        /*
            AsNoTracking = Nao verifica se na tabela as linhas estao modificadas ou nao, apenas ler o conteudo.
            Skip = Ignora um número especificado de elementos em uma sequência e, em seguida, retorna os elementos restantes.
            Take = Retorna um número especificado de elementos contíguos a partir do início de uma sequência.

            Exemplo pratico: temos 20 categorias e cada pagina tem 5 elementos:

            Skip(1-1) * 5) => skip(0), Nao vamos ignorar nenhum elemento
            Take(5) Retorna os primeiros 5 elementos (indice 0 ate 4)

            Skip((2-1)*5) -> skip(5): Ignora os primeiros 5 elementos
            Take(5) retorna os proximos 5 elementos (indice 5 ate o 9)

            E assim sucessivamente.
        */


        try
        {
            var query = context
                .Categories
                .AsNoTracking()
                .OrderBy(x => x.Name);
    
            var categories = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
    
            var count = await query.CountAsync();
    
            return new PagedResponse<List<Category>>(categories,
                count,
                request.PageNumber,
                request.PageSize
            );
        }
        catch 
        {
            return new PagedResponse<List<Category>>(null, 500, "nao foi possivel consultar as categorias");
        }
    }

}