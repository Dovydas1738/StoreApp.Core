using StoreApp.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using StoreApp.Core.Models;

namespace StoreApp.Core.Repositories
{
    public class MongoCacheRepository : IMongoCacheRepository
    {
        private IMongoCollection<Buyer> _buyers;
        private IMongoCollection<Seller> _sellers;
        private IMongoCollection<Order> _orders;

        public MongoCacheRepository(IMongoClient mongoClient)
        {
            _buyers = mongoClient.GetDatabase("buyers").GetCollection<Buyer>("allBuyers");
            _sellers = mongoClient.GetDatabase("sellers").GetCollection<Seller>("allSellers");
            _orders = mongoClient.GetDatabase("orders").GetCollection<Order>("allOrders");
        }

        public async Task AddBuyer(Buyer buyer)
        {
            await _buyers.InsertOneAsync(buyer);
        }

        public async Task AddSeller(Seller seller)
        {
            await _sellers.InsertOneAsync(seller);
        }

        public async Task<List<Buyer>> GetAllBuyers()
        {
            try
            {
                return await _buyers.Find(_ => true).ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Seller>> GetAllSellers()
        {
            try
            {
                return await _sellers.Find(_ => true).ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<Buyer> GetBuyerById(int buyerId)
        {
            try
            {
                return (await _buyers.FindAsync<Buyer>(x => x.BuyerId == buyerId)).First();
            }
            catch
            {
                return null;
            }

        }

        public async Task<Seller> GetSellerById(int sellerId)
        {
            try
            {
                return (await _sellers.FindAsync<Seller>(x => x.SellerId == sellerId)).First();
            }
            catch
            {
                return null;
            }

        }

        public async Task RemoveBuyerById(int buyerId)
        {
            await _buyers.DeleteOneAsync(x => x.BuyerId == buyerId);
        }

        public async Task RemoveSellerById(int sellerId)
        {
            await _sellers.DeleteOneAsync(x => x.SellerId == sellerId);
        }

        public async Task UpdateBuyer(Buyer buyer)
        {
            await _buyers.ReplaceOneAsync(x => x.BuyerId == buyer.BuyerId, buyer);
        }

        public async Task UpdateSeller(Seller seller)
        {
            await _sellers.ReplaceOneAsync(x => x.SellerId == seller.SellerId, seller);
        }

        //orders

        public async Task AddOrder(Order order)
        {
            await _orders.InsertOneAsync(order);
        }

        public async Task<List<Order>> GetAllOrders()
        {
            try
            {
                return await _orders.Find(_ => true).ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            try
            {
                return (await _orders.FindAsync<Order>(x => x.OrderId == orderId)).First();
            }
            catch
            {
                return null;
            }
        }

        public async Task RemoveOrderById(int orderId)
        {
            await _orders.DeleteOneAsync(x => x.OrderId == orderId);
        }

        public async Task UpdateOrder(Order order)
        {
            await _orders.ReplaceOneAsync(x => x.OrderId == order.OrderId, order);
        }
    }
}
