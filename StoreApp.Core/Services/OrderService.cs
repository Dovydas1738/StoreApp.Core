using Microsoft.EntityFrameworkCore;
using StoreApp.Core.Contracts;
using StoreApp.Core.Models;
using StoreApp.Core.Repositories;
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
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IMongoCacheRepository mongoCacheRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _mongoCacheRepository = mongoCacheRepository;
            _productRepository = productRepository;
        }

        public async Task AddOrder(Order order)
        {

            var allProducts = (await _productRepository.GetAllProducts()).ToList();
            Product orderedProduct = allProducts.FirstOrDefault(p => p.ProductId == order.ProductId);

            if (orderedProduct == null || order.Quantity > orderedProduct.AmountInStorage)
            {
                throw new Exception("Not enough products in storage/order not found");
            }
            else
            {
                orderedProduct.AmountInStorage -= order.Quantity;
                await _productRepository.UpdateProduct(orderedProduct);
                await _orderRepository.AddOrder(order);
                await _mongoCacheRepository.AddOrder(order);
            }

        }

        public async Task<List<Order>> GetAllOrders()
        {
            List<Order> dbOrders = await _orderRepository.GetAllOrders();
            List<Order> cacheOrders = await _mongoCacheRepository.GetAllOrders();

            if (cacheOrders.Count < dbOrders.Count)
            {
                await _mongoCacheRepository.ClearOrdersCache();
                foreach (Order o in dbOrders)
                {
                    await _mongoCacheRepository.AddOrder(o);
                }
            }
            return await _mongoCacheRepository.GetAllOrders();

        }

        public async Task<Order> GetOrderById(int orderId)
        {
            Order foundOrder = await _mongoCacheRepository.GetOrderById(orderId);

            if (foundOrder == null)
            {
                Order foundOrder2 = await _orderRepository.GetOrderById(orderId);

                if (foundOrder2 == null)
                {
                    throw new Exception("No order found");
                }
                else
                {
                    await _mongoCacheRepository.AddOrder(foundOrder2);
                }

                return await _mongoCacheRepository.GetOrderById(orderId);
            }
            return foundOrder;

        }

        public async Task RemoveOrderById(int orderId)
        {
            Order foundOrderCache = await _mongoCacheRepository.GetOrderById(orderId);
            Order foundOrder = await _orderRepository.GetOrderById(orderId);

            if (foundOrder == null && foundOrderCache == null)
            {
                throw new Exception("No order found");
            }

            var allProducts = _productRepository.GetAllProducts().Result.ToList();
            Product orderedProduct = allProducts.FirstOrDefault(p => p.ProductId == foundOrder.ProductId);

            orderedProduct.AmountInStorage += foundOrder.Quantity;
            await _productRepository.UpdateProduct(orderedProduct);

            await _orderRepository.RemoveOrderById(orderId);
            await _mongoCacheRepository.RemoveOrderById(orderId);


        }

        public async Task CompleteOrderById(int orderId)
        {
            Order foundOrderCache = await _mongoCacheRepository.GetOrderById(orderId);
            Order foundOrder = await _orderRepository.GetOrderById(orderId);

            if (foundOrder == null && foundOrderCache == null)
            {
                throw new Exception("No order found");
            }

            await _orderRepository.RemoveOrderById(orderId);
            await _mongoCacheRepository.RemoveOrderById(orderId);
        }


        public async Task UpdateOrder(Order order)
        {
            Order foundOrderCache = await _mongoCacheRepository.GetOrderById(order.OrderId);
            Order foundOrder = await _orderRepository.GetOrderById(order.OrderId);

            if (foundOrder == null && foundOrderCache == null)
            {
                throw new Exception("No order found");
            }

            var allProducts = _productRepository.GetAllProducts().Result.ToList();
            Product orderedProduct = allProducts.FirstOrDefault(p => p.ProductId == order.ProductId);

            if (order.Quantity > orderedProduct.AmountInStorage + foundOrder.Quantity)
            {
                throw new Exception("Not enough products in storage/order not found");
            }
            else
            {
                orderedProduct.AmountInStorage = orderedProduct.AmountInStorage + foundOrder.Quantity - order.Quantity;
                await _productRepository.UpdateProduct(orderedProduct);
                await _orderRepository.UpdateOrder(order);
                await _mongoCacheRepository.UpdateOrder(order);
            }
        }
    }
}
