using GLPI.Core.Models;
using GLPI.Core.Requests.Categories;
using GLPI.Core.Responses;

namespace GLPI.Core.Handlers;
public interface ICategoryHandler
{
    Task<Response<Category>> CreateAsync(CreateCategoryRequest request);
    Task<Response<Category>> UpdateAsync(UpdateCategoryRequest request);
    Task<Response<Category>> DeleteAsync(DeleteCategoryRequest request);
    Task<Response<Category>> GetByIdAsync(GetByIdCategoryRequest request);
    Task<Response<List<Category>>> GetAllAsync(GetAllCategoryRequest request);

}