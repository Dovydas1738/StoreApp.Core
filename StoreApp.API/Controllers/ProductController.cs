using Microsoft.AspNetCore.Mvc;
using StoreApp.Core.Models;
using StoreApp.Core.Contracts;
using StoreApp.Core.Repositories;
using StoreApp.Core.Services;
using StoreApp.API.Models;

namespace StoreApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<OrderController> _logger;

        public ProductController(IProductService productService, ILogger<OrderController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpPost("Add a product")]
        public async Task<IActionResult> AddProduct(CreateProductRequest newProduct)
        {
            try
            {
                Product product = new Product(newProduct.ProductName, newProduct.Price, newProduct.Category, newProduct.AmountInStorage);

                await _productService.AddProduct(product);
                _logger.LogInformation("Add order successful");
                return Ok();
            }
            catch
            {
                _logger.LogInformation("Add product FAILED");
                return Problem();
            }
        }

        [HttpGet("Get all products")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var allProducts = await _productService.GetAllProducts();
                _logger.LogInformation("Get all products successful");
                return Ok(allProducts);
            }
            catch
            {
                _logger.LogInformation("Get all products FAILED");
                return Problem();
            }
        }

        [HttpGet("Get product by Id")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductById(id);
                _logger.LogInformation("Get product by id successful");
                return Ok(product);
            }
            catch
            {
                _logger.LogInformation("Get product by id FAILED");
                return Problem();
            }
        }

        [HttpDelete("Delete product by Id")]
        public async Task<IActionResult> DeleteProductById(int id)
        {
            try
            {
                await _productService.RemoveProductById(id);
                _logger.LogInformation("Delete product by id successful");
                return Ok();
            }
            catch
            {
                _logger.LogInformation("Delete product by id FAILED");
                return Problem();
            }
        }

        [HttpPatch("Update a product")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            try
            {
                await _productService.UpdateProduct(product);
                _logger.LogInformation("Update product successful");
                return Ok();
            }
            catch
            {
                _logger.LogInformation("Update product FAILED");
                return Problem();
            }
        }

    }
}
