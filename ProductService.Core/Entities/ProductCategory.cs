
using System.ComponentModel.DataAnnotations;

namespace ProductService.Core.Entities
{
    public class ProductCategory
    {
        [Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string? ParentCategory { get; set; }
        public string? UrlSlug { get; set; }
       
    }
}
