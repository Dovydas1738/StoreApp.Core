using StoreApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Core.Contracts
{
    public interface IMongoCacheRepository
    {
        Task AddBuyer(Buyer buyer);
        Task AddSeller(Seller seller);
        Task RemoveBuyerById(int buyerId);
        Task RemoveSellerById(int sellerId);
        Task<Buyer> GetBuyerById(int buyerId);
        Task<Seller> GetSellerById(int sellerId);
        Task<List<Buyer>> GetAllBuyers();
        Task<List<Seller>> GetAllSellers();
        Task UpdateBuyer(Buyer buyer);
        Task UpdateSeller(Seller seller);
        Task AddOrder(Order order);
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int orderId);
        Task RemoveOrderById(int orderId);
        Task UpdateOrder(Order order);
        Task DropCaches();
        Task ClearBuyersCache();
        Task ClearSellersCache();
        Task ClearOrdersCache();
    }
}
