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
    public class OrderRepository : IOrderRepository
    {
        public async Task AddOrder(Order order)
        {
            using (var context = new StoreAppDbContext())
            {
                await context.Orders.AddAsync(order);

                context.Entry(order).Reference(x => x.Buyer).Load();
                context.Entry(order).Reference(x => x.Product).Load();
                context.Entry(order).Reference(x => x.Seller).Load();

                await context.SaveChangesAsync();
            }
        }

        public async Task RemoveOrderById(int orderId)
        {
            using (var context = new StoreAppDbContext())
            {
                context.Orders.Remove(await context.Orders.FindAsync(orderId));
                await context.SaveChangesAsync();
            }
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            using (var context = new StoreAppDbContext())
            {
                Order foundOrder = await context.Orders.FindAsync(orderId);
                return foundOrder;
            }
        }

        public async Task<List<Order>> GetAllOrders()
        {
            using (var context = new StoreAppDbContext())
            {
                List<Order> allOrders = await context.Orders.ToListAsync();

                foreach (Order o in allOrders)
                {
                    context.Entry(o).Reference(x => x.Buyer).Load();
                    context.Entry(o).Reference(x => x.Product).Load();
                    context.Entry(o).Reference(x => x.Seller).Load();
                    Console.WriteLine($"{o.Buyer.Name} {o.Buyer.Surname} {o.Product.ProductName} {o.Seller.Name} {o.Seller.Surname}");
                }

                return allOrders;
            }
        }

        public async Task UpdateOrder(Order order)
        {
            using (var context = new StoreAppDbContext())
            {
                context.Orders.Update(order);
                await context.SaveChangesAsync();
            }
        }


    }
}
