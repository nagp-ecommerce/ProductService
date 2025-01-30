using Microsoft.Extensions.Configuration;
using ProductService.Application.DTOs;
using ProductService.Application.Interfaces;
using ProductService.Application.Mappings;
using ProductService.Core.Entities;
using SharedService.Lib.Interfaces;
using SharedService.Lib.PubSub;

namespace ProductService.Application.Services
{
    public class ProductInfoService: IProductService
    {
        private IGenericRepository<Product> _productRepository;
        private IGenericRepository<ProductCategory> _categoryRepository;
        private PublisherService _snsClient;
        private string topicArn = String.Empty;
        public ProductInfoService(
            IGenericRepository<Product> productRepository,
            IGenericRepository<ProductCategory> categoryRepository,
            PublisherService snsClient,
            IConfiguration config
        )
        {
            _productRepository=productRepository;
            _categoryRepository=categoryRepository;
            _snsClient=snsClient;
            topicArn = config["SNS:TopicArn"] ?? "";
        }
        public async Task AddProduct(ProductDto productDto) 
        {
            var category = await _categoryRepository.GetByIdAsync(productDto.CategoryId);
            var product = productDto.ToEntity(category);
            await _productRepository.CreateAsync(product);
            var message = new Message<Product>
            { 
                Action = ProductEvent.CREATED,
                Payload = product,
                TimeStamp = DateTime.Now
            };
            await _snsClient.PublishMessageAsync(topicArn, message);
        }
        public async Task UpdateProduct(ProductDto productDto)
        {
            var category = await _categoryRepository.GetByIdAsync(productDto.CategoryId);
            var product = productDto.ToEntity(category);
            await _productRepository.UpdateAsync(product);
            var message = new Message<Product>
            {
                Action = ProductEvent.UPDATED,
                Payload = product,
                TimeStamp = DateTime.Now
            };
            await _snsClient.PublishMessageAsync(topicArn, message);
        }

        public async Task RemoveProduct(ProductDto productDto) 
        {
            await _productRepository.DeleteAsync(productDto.Id);
            var product = productDto.ToEntity(null);
            var message = new Message<Product>
            {
                Action = ProductEvent.DELETED,
                Payload = product,
                TimeStamp = DateTime.Now
            };
            await _snsClient.PublishMessageAsync(topicArn, message);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var res = await _productRepository.GetAllAsync();
            return res;
        }
    }
}
