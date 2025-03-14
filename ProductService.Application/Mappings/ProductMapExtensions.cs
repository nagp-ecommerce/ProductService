﻿using System;
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
            ProductName=productDto.ProductName,
            Price=productDto.Price,
            Description=productDto.Description,
            Brand=productDto.Brand,
            Category= category,
            Discount=0,
            InstockQuanity=10,
            Offers = [],
            ProductReviews = [],
            UrlSlug = $"/{productDto.ProductName}",
            MainImageUrl=productDto.MainImageUrl
        };

        public static ProductDto FromEntity(this Product product) => new ProductDto(
            product.ProductName,
            product.Description,
            product?.Category?.CategoryId ?? 0,
            product.Brand,
            product.Price,
            product.MainImageUrl,
            product.ProductImages is not null
                ? product.ProductImages.Select(x => x.ImageUrl).ToList() 
                : new List<string>() { "" },
            product.ProductId
        );

    }
}
