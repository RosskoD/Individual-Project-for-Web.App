namespace GobelinsWorld.Services.Admin
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GobelinsWorld.Services.Admin.Models;
    using System.Linq;

    public class ProducerService : IProducerService
    {
        private readonly GobelinsWorldDbContext db;

        public ProducerService(GobelinsWorldDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<ProducerNameListingServiceModel>> All()
        {
            return await this.db.Producers
                .Select(p => new ProducerNameListingServiceModel
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ProducerBrandListingServiceModel>> Brands()
        {
            return await this.db.Producers
                .Select(p => new ProducerBrandListingServiceModel
                {
                    Id=p.Id,
                    LogoUrl =p.LogoUrl
                })
                .ToListAsync();
        }

        public async Task Create(string name, string logoUrl)
        {
            var producer = new Producer
            {
                Name = name,
                LogoUrl = logoUrl
            };

            this.db.Add(producer);
            await this.db.SaveChangesAsync();
        }

        public async Task Edit(int id, string name, string logoUrl)
        {
            var producerExist = await this.db.Producers.FindAsync(id);

            if (producerExist==null)
            {
                return;
            }

            producerExist.Name = name;
            producerExist.LogoUrl = logoUrl;

           await this.db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var producerExist = await this.db.Producers.FindAsync(id);

            if (producerExist == null)
            {
                return;
            }

            this.db.Remove(producerExist);
            await this.db.SaveChangesAsync();
        }

        public async Task<ProducerFormServiceModel> FindById(int id)
        {           
            return await this.db.Producers.Where(p=>p.Id==id)
                .Select(p=>new ProducerFormServiceModel
                {
                    Name=p.Name,
                    LogoUrl=p.LogoUrl
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsExist(int id)
        {
            return await this.db.Producers.AnyAsync(p => p.Id == id);
        }
    }
}
