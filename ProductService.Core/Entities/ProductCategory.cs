
namespace ProductService.Core.Entities
{
    public class ProductCategory
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string ParentCategory { get; set; }
        public int UrlSlug { get; set; }
       
    }
}
