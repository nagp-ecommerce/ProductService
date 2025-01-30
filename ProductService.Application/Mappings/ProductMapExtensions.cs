using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService.Application.DTOs;
using ProductService.Core.Entities;

namespace ProductService.Application.Mappings
{
    public static class ProductMapExtensions
    {
        public static Product ToEntity(this ProductDto productDto, ProductCategory category) => new Product()
        {
            ProductId=productDto.Id,
            ProductName=productDto.ProductName,
            Price=productDto.Price,
            Description=productDto.Description,
            Brand=productDto.Brand,
            Category= category,
            Discount=0,
            InstockQuanity=10,
            Offers = [],
            ProductReviews = [],
            UrlSlug = $"/{productDto.ProductName}"
        };

        public static ProductDto FromEntity(this Product product) => new ProductDto(
            product.ProductId,
            product.ProductName,
            product.Description,
            product.Category.CategoryId,
            product.Brand,
            product.Price
        );

    }
}
