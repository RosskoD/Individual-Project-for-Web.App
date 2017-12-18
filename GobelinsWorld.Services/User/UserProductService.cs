namespace GobelinsWorld.Services.User
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserProductService : IUserProductService
    {
        private readonly GobelinsWorldDbContext db;

        public UserProductService(GobelinsWorldDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<UserProductListingServiceModel>> New()
        {
            return await this.db.Products
                .OrderByDescending(p => p.Id)
                .ProjectTo<UserProductListingServiceModel>()
                .Take(9)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserProductListingServiceModel>> AllByCategory(int id, int page = 1, int pageSize = 10)
        {
            return await this.db.Products
                .Where(P => P.CategoryId == id)
                .OrderByDescending(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<UserProductListingServiceModel>()
                .ToListAsync();
        }

        public async Task<IEnumerable<UserProductListingServiceModel>> AllByProducer(int id, int page = 1, int pageSize = 10)
        {
            return await this.db.Products
                .Where(P => P.ProducerId == id)
                .OrderByDescending(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<UserProductListingServiceModel>()
                .ToListAsync();
        }

        public async Task<IEnumerable<UserProductListingServiceModel>> AllBySearch(string searchText, int page = 1, int pageSize = 10)
        {
            searchText = searchText ?? string.Empty;

            return await this.db.Products
                .Where(p => p.Name.ToLower().Contains(searchText.ToLower()))
                .OrderByDescending(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<UserProductListingServiceModel>()
                .ToListAsync();
        }

        public async Task<ProductDetailServiceModel> Detail(int id)
        {
            return await this.db.Products
                 .Where(p => p.Id == id)
                 .ProjectTo<ProductDetailServiceModel>()
                 .FirstOrDefaultAsync();
        }

        public int Total()
        {
            return this.db.Products.Count();
        }
    }
}
