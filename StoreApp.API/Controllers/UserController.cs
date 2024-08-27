using StoreApp.Core.Models;
using StoreApp.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Core.Services;
using StoreApp.API.Models;

namespace StoreApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<OrderController> _logger;

        public UserController(IUserService userService, ILogger<OrderController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // Buyers

        [HttpPost("Add a buyer")]
        public async Task<IActionResult> AddBuyer(CreateBuyerRequest newBuyer)
        {
            try
            {
                Buyer buyer = new Buyer(newBuyer.Name, newBuyer.Surname, newBuyer.Email, newBuyer.PhoneNumber, newBuyer.IsInLoyaltyProgram);

                await _userService.AddBuyer(buyer);
                _logger.LogInformation("Add buyer successful");
                return Ok();
            }
            catch
            {
                _logger.LogInformation("Add buyer FAILED");
                return Problem();
            }
        }

        [HttpGet("Get all buyers")]
        public async Task<IActionResult> GetAllBuyers()
        {
            try
            {
                var allBuyers = await _userService.GetAllBuyers();
                _logger.LogInformation("Get all buyers successful");
                return Ok(allBuyers);
            }
            catch
            {
                _logger.LogInformation("Get all buyers FAILED");
                return Problem();
            }
        }

        [HttpGet("Get buyer by Id")]
        public async Task<IActionResult> GetBuyerById(int id)
        {
            try
            {
                var buyer = await _userService.GetBuyerById(id);
                _logger.LogInformation("Get buyer by id successful");
                return Ok(buyer);
            }
            catch
            {
                _logger.LogInformation("Get buyer by id FAILED");
                return Problem();
            }
        }

        [HttpDelete("Delete buyer by Id")]
        public async Task<IActionResult> DeleteBuyerById(int id)
        {
            try
            {
                await _userService.RemoveBuyerById(id);
                _logger.LogInformation("Delete buyer by id successful");
                return Ok();
            }
            catch
            {
                _logger.LogInformation("Delete buyer by id FAILED");
                return Problem();
            }
        }

        [HttpPatch("Update a buyer")]
        public async Task<IActionResult> UpdateBuyer(Buyer buyer)
        {
            try
            {
                await _userService.UpdateBuyer(buyer);
                _logger.LogInformation("Update buyer successful");
                return Ok();
            }
            catch
            {
                _logger.LogInformation("Update buyer FAILED");
                return Problem();
            }
        }

        // Sellers

        [HttpPost("Add a seller")]
        public async Task<IActionResult> AddSeller(CreateSellerRequest newSeller)
        {
            try
            {
                Seller seller = new Seller(newSeller.Name, newSeller.Surname, newSeller.Email, newSeller.PhoneNumber, newSeller.Position);

                await _userService.AddSeller(seller);
                _logger.LogInformation("Add seller successful");
                return Ok();
            }
            catch
            {
                _logger.LogInformation("Add seller FAILED");
                return Problem();
            }
        }

        [HttpGet("Get all sellers")]
        public async Task<IActionResult> GetAllSellers()
        {
            try
            {
                var allSellers = await _userService.GetAllSellers();
                _logger.LogInformation("Get all sellers successful");
                return Ok(allSellers);
            }
            catch
            {
                _logger.LogInformation("Get all sellers FAILED");
                return Problem();
            }
        }

        [HttpGet("Get seller by Id")]
        public async Task<IActionResult> GetSellerById(int id)
        {
            try
            {
                var seller = await _userService.GetSellerById(id);
                _logger.LogInformation("Get seller by id successful");
                return Ok(seller);
            }
            catch
            {
                _logger.LogInformation("Get seller by id FAILED");
                return Problem();
            }
        }

        [HttpDelete("Delete seller by Id")]
        public async Task<IActionResult> DeleteSellerById(int id)
        {
            try
            {
                await _userService.RemoveSellerById(id);
                _logger.LogInformation("Delete seller by id successful");
                return Ok();
            }
            catch
            {
                _logger.LogInformation("Delete seller by id FAILED");
                return Problem();
            }
        }

        [HttpPatch("Update a seller")]
        public async Task<IActionResult> UpdateSeller(Seller seller)
        {
            try
            {
                await _userService.UpdateSeller(seller);
                _logger.LogInformation("Update seller successful");
                return Ok();
            }
            catch
            {
                _logger.LogInformation("Update seller FAILED");
                return Problem();
            }
        }

    }
}
