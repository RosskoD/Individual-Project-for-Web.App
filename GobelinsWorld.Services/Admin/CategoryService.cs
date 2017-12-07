namespace GobelinsWorld.Services.Admin
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoryService : ICategoryService
    {
        private readonly GobelinsWorldDbContext db;

        public CategoryService(GobelinsWorldDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CategoryFormServiceModel>> All()
        {
            return await this.db.Categories.Select(c => new CategoryFormServiceModel { Id = c.Id, Name = c.Name }).ToListAsync();
        }

        public async Task Create(string name)
        {
            var category = new Category
            {
                Name = name
            };

            this.db.Add(category);
            await this.db.SaveChangesAsync();
        }

        public async Task Edit(int id, string name)
        {
            var existCategory = await this.db.Categories.FindAsync(id);

            if (existCategory == null)
            {
                return;
            }

            existCategory.Name = name;

            await this.db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var existCategory = await this.db.Categories.FindAsync(id);

            if (existCategory == null)
            {
                return;
            }

            this.db.Categories.Remove(existCategory);
            await this.db.SaveChangesAsync();
        }
        public async Task<CategoryFormServiceModel> FindById(int id)
        {
            return await this.db.Categories
                 .Where(c => c.Id == id)
                 .Select(c => new CategoryFormServiceModel { Name = c.Name })
                 .FirstOrDefaultAsync();
        }

        public async Task<bool> Exist(int id)
        {
            return await this.db.Categories.AnyAsync(c => c.Id == id);
        }
    }
}
