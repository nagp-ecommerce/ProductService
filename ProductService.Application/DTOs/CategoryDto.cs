using System.ComponentModel.DataAnnotations;
using ProductService.Core.Entities;

namespace ProductService.Application.DTOs
{
    public record CategoryDto
    (
         [Required] string CategoryName,
         [Required] string Description,
         int ParentCategoryId
    );
}
