using System.ComponentModel.DataAnnotations;

namespace ProductService.Core.Entities
{
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }

        // navigational property
        public Product Product { get; set; }
        public string ImageUrl { get; set; }
        public bool IsThumbnail { get; set; }
        public int? DisplayOrder { get; set; }
    }
}