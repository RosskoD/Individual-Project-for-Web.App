namespace GobelinsWorld.Services.Admin
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class OrderService : IOrderService
    {
        private readonly GobelinsWorldDbContext db;

        public OrderService(GobelinsWorldDbContext db)
        {
            this.db = db;
        }

      public async Task<IEnumerable<OrderListingServiceModel>> All()
        {
            return await this.db.Orders
                .OrderByDescending(o=>o.Id)
                 .Select(o => new OrderListingServiceModel
                 {
                     Id=o.Id,                   
                     UserId=o.UserId,
                     User=o.User,
                     TotalPrice=o.TotalPrice,
                     Items=o.Items
                 })
                 .ToListAsync();
        }
    }
}
