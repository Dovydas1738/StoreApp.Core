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
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, IUserService userService, IProductService productService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _userService = userService;
            _productService = productService;
            _logger = logger;

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
                _logger.LogInformation("Order added successfully");
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _logger.LogInformation("Add order FAILED");
                return Problem(detail: ex.Message);
            }
        }

        [HttpGet("Get all orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var allOrders = await _orderService.GetAllOrders();
                _logger.LogInformation("Get all orders successful");
                return Ok(allOrders);
            }
            catch
            {
                _logger.LogInformation("Get all orders FAILED");
                return Problem();
            }
        }

        [HttpGet("Get order by Id")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var order = await _orderService.GetOrderById(id);
                _logger.LogInformation("Get order by Id successful");
                return Ok(order);
            }
            catch
            {
                _logger.LogInformation("Get order by Id FAILED");
                return Problem();
            }
        }

        [HttpDelete("Delete cancelled order by Id")]
        public async Task<IActionResult> RemoveOrderById(int id)
        {
            try
            {
                await _orderService.RemoveOrderById(id);
                _logger.LogInformation("Cancelled order deletion successful");
                return Ok();
            }
            catch
            {
                _logger.LogInformation("Cancelled order deletion FAILED");
                return Problem();
            }
        }

        [HttpDelete("Complete order by Id")]
        public async Task<IActionResult> CompleteOrderById(int id)
        {
            try
            {
                await _orderService.CompleteOrderById(id);
                _logger.LogInformation("Order completion successful");
                return Ok();
            }
            catch
            {
                _logger.LogInformation("Order completion FAILED");
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
                _logger.LogInformation("Order update successful");
                return Ok();
            }
            catch
            {
                _logger.LogInformation("Order update FAILED");
                return Problem();
            }
        }

    }
}
