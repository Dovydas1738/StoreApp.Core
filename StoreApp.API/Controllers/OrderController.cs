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
                var buyer = await _userService.GetBuyerById(newOrder.BuyerId);
                var product = await _productService.GetProductById(newOrder.ProductId);
                var seller = await _userService.GetSellerById(newOrder.SellerId);

                if (buyer == null || product == null || seller == null)
                {
                    return NotFound("One or more entities not found.");
                }

                var order = new Order(buyer, product, newOrder.Quantity, seller);



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

        [HttpDelete("Complete order by Id")]
        public async Task<IActionResult> CompleteOrderById(int id)
        {
            try
            {
                await _orderService.CompleteOrderById(id);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }


        [HttpPatch("Update an order")]
        public async Task<IActionResult> UpdateOrder(UpdateOrderRequest newOrder)
        {
            try
            {

                var buyer = await _userService.GetBuyerById(newOrder.BuyerId);
                var product = await _productService.GetProductById(newOrder.ProductId);
                var seller = await _userService.GetSellerById(newOrder.SellerId);

                if (buyer == null || product == null || seller == null)
                {
                    return NotFound("One or more entities not found.");
                }

                var order = new Order(buyer, product, newOrder.Quantity, seller);

                order.OrderId = newOrder.OrderId;

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
