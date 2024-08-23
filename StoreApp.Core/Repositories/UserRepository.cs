using Microsoft.EntityFrameworkCore;
using StoreApp.Core.Contracts;
using StoreApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task AddBuyer(Buyer buyer)
        {
            using (var context = new StoreAppDbContext())
            {
                await context.Buyers.AddAsync(buyer);
                await context.SaveChangesAsync();
            }
        }

        public async Task AddSeller(Seller seller)
        {
            using (var context = new StoreAppDbContext())
            {
                await context.Sellers.AddAsync(seller);
                await context.SaveChangesAsync();
            }

        }

        public async Task RemoveBuyerById(int buyerId)
        {
            using (var context = new StoreAppDbContext())
            {
                context.Buyers.Remove(await context.Buyers.FindAsync(buyerId));
                await context.SaveChangesAsync();
            }
        }

        public async Task RemoveSellerById(int sellerId)
        {
            using (var context = new StoreAppDbContext())
            {
                context.Sellers.Remove(await context.Sellers.FindAsync(sellerId));
                await context.SaveChangesAsync();
            }
        }

        public async Task<Buyer> GetBuyerById(int buyerId)
        {
            using (var context = new StoreAppDbContext())
            {
                Buyer foundBuyer = await context.Buyers.FindAsync(buyerId);
                return foundBuyer;
            }
        }

        public async Task<Seller> GetSellerById(int sellerId)
        {
            using (var context = new StoreAppDbContext())
            {
                Seller foundSeller = await context.Sellers.FindAsync(sellerId);
                return foundSeller;
            }
        }

        public async Task<List<Buyer>> GetAllBuyers()
        {
            using (var context = new StoreAppDbContext())
            {
                List<Buyer> allBuyers = await context.Buyers.ToListAsync();
                return allBuyers;
            }
        }

        public async Task<List<Seller>> GetAllSellers()
        {
            using (var context = new StoreAppDbContext())
            {
                List<Seller> allSellers = await context.Sellers.ToListAsync();
                return allSellers;
            }
        }

        public async Task UpdateBuyer(Buyer buyer)
        {
            using (var context = new StoreAppDbContext())
            {
                context.Buyers.Update(buyer);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateSeller(Seller seller)
        {
            using (var context = new StoreAppDbContext())
            {
                context.Sellers.Update(seller);
                await context.SaveChangesAsync();
            }
        }

    }
}
