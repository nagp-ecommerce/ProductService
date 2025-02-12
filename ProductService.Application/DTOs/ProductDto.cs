using System.ComponentModel.DataAnnotations;
using ProductService.Core.Entities;

namespace ProductService.Application.DTOs
{
    public record ProductDto
    (
         [Required] string ProductName,
         [Required] string Description,
         [Required] int CategoryId,
         [Required] string Brand,
         [Required, DataType(DataType.Currency)] double Price,
         [Required] string MainImageUrl,
         List<string>? ProductImages,
         int Id
    );
}
