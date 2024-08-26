using Microsoft.AspNetCore.Mvc;
using StoreApp.Core.Models;
using StoreApp.Core.Contracts;
using StoreApp.Core.Repositories;
using StoreApp.Core.Services;
using StoreApp.API.Models;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace StoreApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public OrderController(IOrderService orderService, IUserService userService, IProductService productService)
        {
            _orderService = orderService;
            _userService = userService;
            _productService = productService;
        }


        [HttpPost("Create an order")]
        public async Task<IActionResult> AddOrder(CreateOrderRequest newOrder)
        {
            try
            {
                Order order = new Order();

                order.Buyer = new Buyer(newOrder.BuyerId);
                order.Product = new Product(newOrder.ProductId);
                order.Quantity = newOrder.Quantity;
                order.DateTime = DateTime.Now;
                order.Seller = new Seller(newOrder.SellerId);



                await _orderService.AddOrder(order);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Problem(detail: ex.Message);
            }
        }

        [HttpGet("Get all orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var allOrders = await _orderService.GetAllOrders();
                return Ok(allOrders);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpGet("Get order by Id")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var order = await _orderService.GetOrderById(id);
                return Ok(order);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpDelete("Delete cancelled order by Id")]
        public async Task<IActionResult> RemoveOrderById(int id)
        {
            try
            {
                await _orderService.RemoveOrderById(id);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPatch("Update an order")]
        public async Task<IActionResult> UpdateOrder(CreateOrderRequest newOrder)
        {
            try
            {
                Order order = new Order
                {
                    Buyer = new Buyer { BuyerId = newOrder.BuyerId },
                    Product = new Product { ProductId = newOrder.ProductId },
                    Quantity = newOrder.Quantity,
                    DateTime = DateTime.Now,
                    Seller = new Seller { SellerId = newOrder.SellerId },
                };


                await _orderService.UpdateOrder(order);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

    }
}
