using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductService.Core.Entities;

namespace ProductService.Infrastructure.Data
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options)
        : DbContext(options)
    {
        public  DbSet<Product> Products { get; set; }
        public  DbSet<ProductCategory> Categories { get; set; }
        public  DbSet<ProductOffer> ProductOffers { get; set; }
        public  DbSet<ProductReview> ProductReviews { get; set; }
        public  DbSet<ProductImage> ProductImages { get; set; }

    }
}
