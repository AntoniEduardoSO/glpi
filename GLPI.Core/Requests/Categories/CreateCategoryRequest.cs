using System.ComponentModel.DataAnnotations;

namespace GLPI.Core.Requests.Categories;
public class CreateCategoryRequest : Request
{
    [Required(ErrorMessage = "Nome invalido")]
    [MaxLength(16, ErrorMessage = "Categoria tem que possuir 16 caracteres no maximo")]
    public string Name { get; set; } = string.Empty; 

    [Required(ErrorMessage = "Color invalido")]
    [MaxLength(7, ErrorMessage = "a cor deve ter no maximo 7 caracteres")]
    public string Color { get; set; } = string.Empty;
}
