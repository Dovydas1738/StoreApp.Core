using StoreApp.Core.Contracts;
using StoreApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMongoCacheRepository _mongoCacheRepository;

        public OrderService(IOrderRepository orderRepository, IMongoCacheRepository mongoCacheRepository)
        {
            _orderRepository = orderRepository;
            _mongoCacheRepository = mongoCacheRepository;
        }

        public Task AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderById(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveOrderById(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
