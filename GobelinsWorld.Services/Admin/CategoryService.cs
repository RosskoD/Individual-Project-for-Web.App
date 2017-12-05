namespace GobelinsWorld.Services.Admin
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CategoryService : ICategoryService
    {
        private readonly GobelinsWorldDbContext db;

        public CategoryService(GobelinsWorldDbContext db)
        {
            this.db = db;
        }

        public async Task<IList<Category>> All()
        {
            return await this.db.Categories.ToListAsync();
        }

    }
}
