namespace GobelinsWorld.Services.Admin
{
    using Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProducerService
    {
        Task<IList<Producer>> All();
    }
}
