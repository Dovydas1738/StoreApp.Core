using StoreApp.Core.Contracts;
using StoreApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMongoCacheRepository _userMongoCacheRepository;

        public UserService(IUserRepository userRepository, IMongoCacheRepository userMongoCacheRepository)
        {
            _userRepository = userRepository;
            _userMongoCacheRepository = userMongoCacheRepository;
        }

        public async Task AddBuyer(Buyer buyer)
        {
            await _userMongoCacheRepository.AddBuyer(buyer);
            await _userRepository.AddBuyer(buyer);
        }

        public async Task AddSeller(Seller seller)
        {
            await _userMongoCacheRepository.AddSeller(seller);
            await _userRepository.AddSeller(seller);
        }

        public async Task<List<Buyer>> GetAllBuyers()
        {
            if (_userMongoCacheRepository.GetAllBuyers().Result.Count < _userRepository.GetAllBuyers().Result.Count)
            {
                //add range i guess, arba replace or some shit
            }

            return await _userMongoCacheRepository.GetAllBuyers();
        }

        public async Task<List<Seller>> GetAllSellers()
        {
            if (_userMongoCacheRepository.GetAllSellers().Result.Count < _userRepository.GetAllSellers().Result.Count)
            {
                //add range i guess
            }

            return await _userMongoCacheRepository.GetAllSellers();
        }

        public async Task<Buyer> GetBuyerById(int buyerId)
        {
            Buyer foundBuyer = await _userMongoCacheRepository.GetBuyerById(buyerId);

            if (foundBuyer == null)
            {
                Buyer foundBuyer2 = await _userRepository.GetBuyerById(buyerId);

                if (foundBuyer2 == null)
                {
                    throw new Exception("No buyer found");
                }
                else
                {
                    await _userMongoCacheRepository.AddBuyer(foundBuyer2);
                }

                return await _userMongoCacheRepository.GetBuyerById(buyerId);
            }
            return foundBuyer;
        }

        public async Task<Seller> GetSellerById(int sellerId)
        {
            Seller foundSeller = await _userMongoCacheRepository.GetSellerById(sellerId);

            if (foundSeller == null)
            {
                Seller foundSeller2 = await _userRepository.GetSellerById(sellerId);

                if (foundSeller2 == null)
                {
                    throw new Exception("No seller found");
                }
                else
                {
                    await _userMongoCacheRepository.AddSeller(foundSeller2);
                }

                return await _userMongoCacheRepository.GetSellerById(sellerId);
            }
            return foundSeller;
        }

        public async Task RemoveBuyerById(int buyerId)
        {
            Buyer foundBuyer = await _userMongoCacheRepository.GetBuyerById(buyerId);
            Buyer foundBuyer2 = await _userRepository.GetBuyerById(buyerId);

            if (foundBuyer == null && foundBuyer2 == null)
            {
                throw new Exception("No buyer found");
            }
            await _userRepository.RemoveBuyerById(buyerId);
            await _userMongoCacheRepository.RemoveBuyerById(buyerId);
        }

        public async Task RemoveSellerById(int sellerId)
        {
            Seller foundSeller = await _userMongoCacheRepository.GetSellerById(sellerId);
            Seller foundSeller2 = await _userRepository.GetSellerById(sellerId);

            if (foundSeller == null && foundSeller2 == null)
            {
                throw new Exception("No buyer found");
            }
            await _userRepository.RemoveSellerById(sellerId);
            await _userMongoCacheRepository.RemoveSellerById(sellerId);

        }

        public Task UpdateBuyer(Buyer buyer)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSeller(Seller seller)
        {
            throw new NotImplementedException();
        }
    }
}
