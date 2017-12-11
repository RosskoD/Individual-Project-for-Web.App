namespace GobelinsWorld.Services.Admin
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using GobelinsWorld.Services.Admin.Models;
    using GobelinsWorld.Services.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly GobelinsWorldDbContext db;

        public ProductService(GobelinsWorldDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<ProductListingServiceModel>> All()
        {
            return await this.db.Products
                .OrderBy(p => p.Category.Id)
                .ThenBy(p => p.ProducerId)
                .ThenByDescending(p => p.Id).ProjectTo<ProductListingServiceModel>()
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductByCategoryListingServiceModel>> AllByCategory(int id, int page=1, int pageSize=10)
        {
            return await this.db.Products
                .Where(P=>P.CategoryId==id)
                .OrderByDescending(p => p.Id)
                .Skip((page-1)*pageSize)
                .Take(pageSize)
                .ProjectTo<ProductByCategoryListingServiceModel>()
                .ToListAsync();
        }

        public int Total()
        {
            return this.db.Products.Count();
        }

        public async Task Create(string name, string code, double weight, decimal price, string description, string imageUrl, int producerId, int categoryId)
        {
            var product = new Product
            {
                Name=name,
                Code=code,
                Weight=weight,
                Price=price,
                Description=description,
                ImageUrl=imageUrl,
                ProducerId=producerId,
                CategoryId=categoryId
            };

            this.db.Add(product);
            await this.db.SaveChangesAsync();
        }

        public async Task Edit(int id, string name, string code, double weight, decimal price, string description, string imageUrl, int producerId, int categoryId)
        {
            var productExist =await this.db.Products.FindAsync(id);

            if (productExist==null)
            {
                return;
            }

            productExist.Name = name;
            productExist.Code = code;
            productExist.Weight = weight;
            productExist.Price = price;
            productExist.Description = description;
            productExist.ImageUrl = imageUrl;
            productExist.ProducerId = producerId;
            productExist.CategoryId = categoryId;

            await this.db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var productExist = await this.db.Products.FindAsync(id);

            if (productExist==null)
            {
                return;
            }

             this.db.Remove(productExist);
            await this.db.SaveChangesAsync();
        }

        public async Task<ProductFormServiceModel> FindById(int id)
        {
            return await this.db.Products.Where(p => p.Id == id).ProjectTo<ProductFormServiceModel>().FirstOrDefaultAsync();
        }

        public async Task<bool> IsExist(int id)
        {
            return await this.db.Products.AnyAsync(p => p.Id == id);
        }
    }
}
