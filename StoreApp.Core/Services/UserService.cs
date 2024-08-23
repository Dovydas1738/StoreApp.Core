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

        public Task AddBuyer(Buyer buyer)
        {
            throw new NotImplementedException();
        }

        public Task AddSeller(Seller seller)
        {
            throw new NotImplementedException();
        }

        public Task<List<Buyer>> GetAllBuyers()
        {
            throw new NotImplementedException();
        }

        public Task<List<Seller>> GetAllSellers()
        {
            throw new NotImplementedException();
        }

        public Task<Buyer> GetBuyerById(int buyerId)
        {
            throw new NotImplementedException();
        }

        public Task<Seller> GetSellerById(int sellerId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveBuyerById(int buyerId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveSellerById(int sellerId)
        {
            throw new NotImplementedException();
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
