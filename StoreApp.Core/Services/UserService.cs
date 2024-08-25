using MongoDB.Driver.Core.Events;
using MongoDB.Driver.Core.Servers;
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
            foreach (Buyer b in await _userRepository.GetAllBuyers())
            {
                if (buyer.Name == b.Name && buyer.Surname == b.Surname)
                {
                    await _userMongoCacheRepository.AddBuyer(buyer);
                    throw new Exception("Buyer already exists.");
                }
            }

            await _userMongoCacheRepository.AddBuyer(buyer);
            await _userRepository.AddBuyer(buyer);
        }

        public async Task AddSeller(Seller seller)
        {
            foreach (Seller b in await _userRepository.GetAllSellers())
            {
                if (seller.Name == b.Name && seller.Surname == b.Surname)
                {
                    await _userMongoCacheRepository.AddSeller(seller);
                    throw new Exception("Seller already exists.");
                }
            }

            await _userMongoCacheRepository.AddSeller(seller);
            await _userRepository.AddSeller(seller);
        }

        public async Task<List<Buyer>> GetAllBuyers()
        {
            List<Buyer> dbBuyers = await _userRepository.GetAllBuyers();
            List<Buyer> cacheBuyers = await _userMongoCacheRepository.GetAllBuyers();

            if (cacheBuyers.Count < dbBuyers.Count)
            {
                await _userMongoCacheRepository.ClearBuyersCache();
                foreach(Buyer b in dbBuyers)
                {
                    await _userMongoCacheRepository.AddBuyer(b);
                }
            }
            return await _userMongoCacheRepository.GetAllBuyers();
        }

        public async Task<List<Seller>> GetAllSellers()
        {
            List<Seller> dbSellers = await _userRepository.GetAllSellers();
            List<Seller> cacheSellers = await _userMongoCacheRepository.GetAllSellers();

            if (cacheSellers.Count < dbSellers.Count)
            {
                await _userMongoCacheRepository.ClearSellersCache();
                foreach(Seller s in dbSellers)
                {
                    await _userMongoCacheRepository.AddSeller(s);
                }
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

        public async Task UpdateBuyer(Buyer buyer)
        {
            Buyer foundBuyer = await _userMongoCacheRepository.GetBuyerById(buyer.BuyerId);
            Buyer foundBuyer2 = await _userRepository.GetBuyerById(buyer.BuyerId);

            if (foundBuyer == null && foundBuyer2 == null)
            {
                throw new Exception("No buyer found");
            }
            await _userRepository.UpdateBuyer(buyer);
            await _userMongoCacheRepository.UpdateBuyer(buyer);
        }

        public async Task UpdateSeller(Seller seller)
        {
            Seller foundSeller = await _userMongoCacheRepository.GetSellerById(seller.SellerId);
            Seller foundSeller2 = await _userRepository.GetSellerById(seller.SellerId);

            if (foundSeller == null && foundSeller2 == null)
            {
                throw new Exception("No buyer found");
            }
            await _userRepository.UpdateSeller(seller);
            await _userMongoCacheRepository.UpdateSeller(seller);

        }
    }
}
