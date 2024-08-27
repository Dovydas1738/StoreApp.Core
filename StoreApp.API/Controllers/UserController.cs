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

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Buyers

        [HttpPost("Add a buyer")]
        public async Task<IActionResult> AddBuyer(CreateBuyerRequest newBuyer)
        {
            try
            {
                Buyer buyer = new Buyer(newBuyer.Name, newBuyer.Surname, newBuyer.Email, newBuyer.PhoneNumber, newBuyer.IsInLoyaltyProgram);

                await _userService.AddBuyer(buyer);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpGet("Get all buyers")]
        public async Task<IActionResult> GetAllBuyers()
        {
            try
            {
                var allBuyers = await _userService.GetAllBuyers();
                return Ok(allBuyers);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpGet("Get buyer by Id")]
        public async Task<IActionResult> GetBuyerById(int id)
        {
            try
            {
                var buyer = await _userService.GetBuyerById(id);
                return Ok(buyer);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpDelete("Delete buyer by Id")]
        public async Task<IActionResult> DeleteBuyerById(int id)
        {
            try
            {
                await _userService.RemoveBuyerById(id);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPatch("Update a buyer")]
        public async Task<IActionResult> UpdateBuyer(Buyer buyer)
        {
            try
            {
                await _userService.UpdateBuyer(buyer);
                return Ok();
            }
            catch
            {
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
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpGet("Get all sellers")]
        public async Task<IActionResult> GetAllSellers()
        {
            try
            {
                var allSellers = await _userService.GetAllSellers();
                return Ok(allSellers);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpGet("Get seller by Id")]
        public async Task<IActionResult> GetSellerById(int id)
        {
            try
            {
                var seller = await _userService.GetSellerById(id);
                return Ok(seller);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpDelete("Delete seller by Id")]
        public async Task<IActionResult> DeleteSellerById(int id)
        {
            try
            {
                await _userService.RemoveSellerById(id);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPatch("Update a seller")]
        public async Task<IActionResult> UpdateSeller(Seller seller)
        {
            try
            {
                await _userService.UpdateSeller(seller);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

    }
}
