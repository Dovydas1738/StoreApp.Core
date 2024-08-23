using StoreApp.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreApp.Core.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public async Task AddProduct(Product product)
        {
            using (var context = new StoreAppDbContext())
            {
                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();
            }
        }

        public async Task RemoveProductById(int productId)
        {
            using (var context = new StoreAppDbContext())
            {
                context.Products.Remove(await context.Products.FindAsync(productId));
                await context.SaveChangesAsync();
            }
        }

        public async Task<Product> GetProductById(int productId)
        {
            using (var context = new StoreAppDbContext())
            {
                Product foundProduct = await context.Products.FindAsync(productId);
                return foundProduct;
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            using (var context = new StoreAppDbContext())
            {
                List<Product> allProducts = await context.Products.ToListAsync();
                return allProducts;
            }
        }

        public async Task UpdateProduct(Product product)
        {
            using (var context = new StoreAppDbContext())
            {
                context.Products.Update(product);
                await context.SaveChangesAsync();  
            }

        }
    }
}
