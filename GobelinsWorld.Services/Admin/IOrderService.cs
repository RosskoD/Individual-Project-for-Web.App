namespace GobelinsWorld.Services.Admin
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrderService 
    {
        Task<IEnumerable<OrderListingServiceModel>> All();
    }
}
