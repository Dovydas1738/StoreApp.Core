using StoreApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Core.Contracts
{
    public interface IOrderService
    {
        Task AddOrder(Order order);
        Task RemoveOrderById(int orderId);
        Task<Order> GetOrderById(int orderId);
        Task<List<Order>> GetAllOrders();
        Task UpdateOrder(Order order);
        Task CompleteOrderById(int orderId);
    }
}
