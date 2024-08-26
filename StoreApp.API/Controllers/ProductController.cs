using Microsoft.AspNetCore.Mvc;
using StoreApp.Core.Models;
using StoreApp.Core.Contracts;
using StoreApp.Core.Repositories;
using StoreApp.Core.Services;

namespace StoreApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("Add a product")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            try
            {
                await _productService.AddProduct(product);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpGet("Get all products")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var allProducts = await _productService.GetAllProducts();
                return Ok(allProducts);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpGet("Get product by Id")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductById(id);
                return Ok(product);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpDelete("Delete product by Id")]
        public async Task<IActionResult> DeleteProductById(int id)
        {
            try
            {
                await _productService.RemoveProductById(id);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPatch("Update a product")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            try
            {
                await _productService.UpdateProduct(product);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

    }
}
