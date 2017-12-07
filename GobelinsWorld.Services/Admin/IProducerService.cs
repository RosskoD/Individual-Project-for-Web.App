namespace GobelinsWorld.Services.Admin
{   
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProducerService
    {
        Task<IEnumerable<ProducerNameListingServiceModel>> All();

        Task<IEnumerable<ProducerBrandListingServiceModel>> Brands();

        Task Create(string name, string logoUrl);

        Task Edit(int id, string name, string logoUrl);

        Task Delete(int id);

        Task<bool> IsExist(int id);

        Task<ProducerFormServiceModel> FindById(int id);
    }
}
