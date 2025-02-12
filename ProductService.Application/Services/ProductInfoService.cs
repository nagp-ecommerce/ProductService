using Amazon.SimpleNotificationService.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProductService.Application.DTOs;
using ProductService.Application.Interfaces;
using ProductService.Application.Mappings;
using ProductService.Core.Entities;
using SharedService.Lib.Interfaces;
using SharedService.Lib.PubSub;

namespace ProductService.Application.Services
{
    public class ProductInfoService : IProductService
    {
        private IGenericRepository<Product> _productRepository;
        private ICategoryRepository _categoryRepository;
        private IPublisherService _PublisherService;
        private string topicArn;
        private ILogger<ProductInfoService> _logger;

        public ProductInfoService(
            IGenericRepository<Product> productRepository,
            ICategoryRepository categoryRepository,
            IPublisherService PublisherService,
            IConfiguration config,
            ILogger<ProductInfoService> logger
        )
        {
            _productRepository = productRepository
                ?? throw new ArgumentNullException(nameof(productRepository));
            _categoryRepository = categoryRepository;
            _PublisherService = PublisherService
                ?? throw new ArgumentNullException(nameof(PublisherService));
            topicArn = config["SNS:TopicArn"] ?? "";
            _logger = logger;
        }
        public async Task<Response> AddProduct(ProductDto productDto)
        {
            var category = await _categoryRepository.GetByIdAsync(productDto.CategoryId);
            var product = productDto.ToEntity(category);
            var response = await _productRepository.CreateAsync(product);
            var message = new Message<Product>
            {
                Action = ProductEvent.CREATED,
                Payload = product,
                TimeStamp = DateTime.Now
            };
            if (response.Success)
            {
                await _PublisherService.PublishMessageAsync(topicArn, message);
            }
            return response;
        }

        public async Task<Response> UpdateProduct(ProductDto productDto)
        {
            var category = await _categoryRepository.GetByIdAsync(productDto.CategoryId);
            var product = productDto.ToEntity(category);
            var response = await _productRepository.UpdateAsync(product);
            var message = new Message<Product>
            {
                Action = ProductEvent.UPDATED,
                Payload = product,
                TimeStamp = DateTime.Now
            };
            if (response.Success)
            {
                await _PublisherService.PublishMessageAsync(topicArn, message);
            }
            return response;
        }

        public async Task<Response> RemoveProduct(ProductDto productDto)
        {
            if (productDto.Id > 0)
            {
                var response = await _productRepository.DeleteAsync(productDto.Id);
                var product = productDto.ToEntity(null);
                var message = new Message<Product>
                {
                    Action = ProductEvent.DELETED,
                    Payload = product,
                    TimeStamp = DateTime.Now
                };
                if (response.Success)
                {
                    await _PublisherService.PublishMessageAsync(topicArn, message);
                }
                return response;
            }
            return new Response { Success = false, Message = "Send correct product ID" };
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var res = await _productRepository.GetAllAsync();
            var list = res.Select(p => p.FromEntity()).ToList();
            return list;
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product.FromEntity();
        }
    }
}
