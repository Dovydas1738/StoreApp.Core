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
        private readonly IMongoCacheRepository _mongoCacheRepository;

        public UserService(IUserRepository userRepository, IMongoCacheRepository userMongoCacheRepository)
        {
            _userRepository = userRepository;
            _mongoCacheRepository = userMongoCacheRepository;
        }

        public async Task AddBuyer(Buyer buyer)
        {
            foreach (Buyer b in await _userRepository.GetAllBuyers())
            {
                if (buyer.Name == b.Name && buyer.Surname == b.Surname)
                {
                    await _mongoCacheRepository.AddBuyer(buyer);
                    throw new Exception("Buyer already exists.");
                }
            }

            await _userRepository.AddBuyer(buyer);
            await _mongoCacheRepository.AddBuyer(buyer);
        }

        public async Task AddSeller(Seller seller)
        {
            foreach (Seller b in await _userRepository.GetAllSellers())
            {
                if (seller.Name == b.Name && seller.Surname == b.Surname)
                {
                    await _mongoCacheRepository.AddSeller(seller);
                    throw new Exception("Seller already exists.");
                }
            }

            await _userRepository.AddSeller(seller);
            await _mongoCacheRepository.AddSeller(seller);
        }

        public async Task<List<Buyer>> GetAllBuyers()
        {
            List<Buyer> dbBuyers = await _userRepository.GetAllBuyers();
            List<Buyer> cacheBuyers = await _mongoCacheRepository.GetAllBuyers();

            if (cacheBuyers.Count < dbBuyers.Count)
            {
                await _mongoCacheRepository.ClearBuyersCache();
                foreach(Buyer b in dbBuyers)
                {
                    await _mongoCacheRepository.AddBuyer(b);
                }
            }
            return await _mongoCacheRepository.GetAllBuyers();
        }

        public async Task<List<Seller>> GetAllSellers()
        {
            List<Seller> dbSellers = await _userRepository.GetAllSellers();
            List<Seller> cacheSellers = await _mongoCacheRepository.GetAllSellers();

            if (cacheSellers.Count < dbSellers.Count)
            {
                await _mongoCacheRepository.ClearSellersCache();
                foreach(Seller s in dbSellers)
                {
                    await _mongoCacheRepository.AddSeller(s);
                }
            }
            return await _mongoCacheRepository.GetAllSellers();
        }

        public async Task<Buyer> GetBuyerById(int buyerId)
        {
            Buyer foundBuyer = await _mongoCacheRepository.GetBuyerById(buyerId);

            if (foundBuyer == null)
            {
                Buyer foundBuyer2 = await _userRepository.GetBuyerById(buyerId);

                if (foundBuyer2 == null)
                {
                    throw new Exception("No buyer found");
                }
                else
                {
                    await _mongoCacheRepository.AddBuyer(foundBuyer2);
                }

                return await _mongoCacheRepository.GetBuyerById(buyerId);
            }
            return foundBuyer;
        }

        public async Task<Seller> GetSellerById(int sellerId)
        {
            Seller foundSeller = await _mongoCacheRepository.GetSellerById(sellerId);

            if (foundSeller == null)
            {
                Seller foundSeller2 = await _userRepository.GetSellerById(sellerId);

                if (foundSeller2 == null)
                {
                    throw new Exception("No seller found");
                }
                else
                {
                    await _mongoCacheRepository.AddSeller(foundSeller2);
                }

                return await _mongoCacheRepository.GetSellerById(sellerId);
            }
            return foundSeller;
        }

        public async Task RemoveBuyerById(int buyerId)
        {
            Buyer foundBuyer = await _mongoCacheRepository.GetBuyerById(buyerId);
            Buyer foundBuyer2 = await _userRepository.GetBuyerById(buyerId);

            if (foundBuyer == null && foundBuyer2 == null)
            {
                throw new Exception("No buyer found");
            }
            await _userRepository.RemoveBuyerById(buyerId);
            await _mongoCacheRepository.RemoveBuyerById(buyerId);
        }

        public async Task RemoveSellerById(int sellerId)
        {
            Seller foundSeller = await _mongoCacheRepository.GetSellerById(sellerId);
            Seller foundSeller2 = await _userRepository.GetSellerById(sellerId);

            if (foundSeller == null && foundSeller2 == null)
            {
                throw new Exception("No buyer found");
            }
            await _userRepository.RemoveSellerById(sellerId);
            await _mongoCacheRepository.RemoveSellerById(sellerId);

        }

        public async Task UpdateBuyer(Buyer buyer)
        {
            Buyer foundBuyer = await _mongoCacheRepository.GetBuyerById(buyer.BuyerId);
            Buyer foundBuyer2 = await _userRepository.GetBuyerById(buyer.BuyerId);

            if (foundBuyer == null && foundBuyer2 == null)
            {
                throw new Exception("No buyer found");
            }
            await _userRepository.UpdateBuyer(buyer);
            await _mongoCacheRepository.UpdateBuyer(buyer);
        }

        public async Task UpdateSeller(Seller seller)
        {
            Seller foundSeller = await _mongoCacheRepository.GetSellerById(seller.SellerId);
            Seller foundSeller2 = await _userRepository.GetSellerById(seller.SellerId);

            if (foundSeller == null && foundSeller2 == null)
            {
                throw new Exception("No buyer found");
            }
            await _userRepository.UpdateSeller(seller);
            await _mongoCacheRepository.UpdateSeller(seller);

        }
    }
}
