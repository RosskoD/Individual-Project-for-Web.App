namespace GobelinsWorld.Services.Admin
{
    using Data;
    using Data.Models;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly GobelinsWorldDbContext db;

        public ProductService(GobelinsWorldDbContext db)
        {
            this.db = db;
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
    }
}
