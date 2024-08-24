using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Core.Contracts;
using StoreApp.Core.Models;

namespace StoreApp.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task AddProduct(Product product)
        {
            foreach (Product p in await _productRepository.GetAllProducts())
            {
                if (product.ProductName == p.ProductName)
                {
                    p.AmountInStorage += product.AmountInStorage;
                    return;
                }
            }
            await _productRepository.AddProduct(product);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }

        public async Task<Product> GetProductById(int productId)
        {
            Product foundProduct = await _productRepository.GetProductById(productId);
            if (foundProduct != null)
            {
                return foundProduct;
            }
            else
            {
                throw new Exception("No product found.");
            }
        }

        public async Task RemoveProductById(int productId)
        {
            Product foundProduct = await _productRepository.GetProductById(productId);
            if (foundProduct != null)
            {
                await _productRepository.RemoveProductById(productId);
            }
            else
            {
                throw new Exception("No product found.");
            }
        }

        public async Task UpdateProduct(Product product)
        {
            Product foundProduct = await _productRepository.GetProductById(product.ProductId);
            if (foundProduct != null)
            {
                await _productRepository.UpdateProduct(product);
            }
            else
            {
                throw new Exception("No product found.");
            }
        }
    }
}
