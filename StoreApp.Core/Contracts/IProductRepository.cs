using StoreApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Core.Contracts
{
    public interface IProductRepository
    {
        Task AddProduct(Product product);
        Task RemoveProductById(int productId);
        Task<Product> GetProductById(int productId);
        Task<List<Product>> GetAllProducts();
        Task UpdateProduct(Product product);
    }
}
