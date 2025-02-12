
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductService.Core.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public string? Description { get; set; }

        [JsonIgnore]
        public ProductCategory Category { get; set; }
        public int InstockQuanity { get; set; }
        public double Price { get; set; }
        public List<ProductReview>? ProductReviews { get; set; }
        public string? UrlSlug { get; set; }
        public string? Brand { get; set; }
        public double? Discount { get; set; }
        public List<ProductOffer>? Offers { get; set; }

        public string MainImageUrl { get; set; } = "https://shop.azelab.com/images/home-4/1.jpg";
        public List<ProductImage>? ProductImages{ get; set; }
    }
}
