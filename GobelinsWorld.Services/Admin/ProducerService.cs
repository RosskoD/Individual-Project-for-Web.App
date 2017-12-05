namespace GobelinsWorld.Services.Admin
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProducerService : IProducerService
    {
        private readonly GobelinsWorldDbContext db;

        public ProducerService(GobelinsWorldDbContext db)
        {
            this.db = db;
        }

        public async Task<IList<Producer>> All()
        {
           return await this.db.Producers.ToListAsync();
        }
    }
}
