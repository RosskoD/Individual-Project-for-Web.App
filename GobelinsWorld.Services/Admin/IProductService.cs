namespace GobelinsWorld.Services.Admin
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductService
    {
        Task<IEnumerable<ProductListingServiceModel>> All();       

        Task Create(string name, string code, double weight, decimal price, string description,
             string imageUrl,int producerId, int categoryId );

        Task Edit(int id, string name, string code, double weight, decimal price, string description,
             string imageUrl, int producerId, int categoryId);

        Task Delete(int id);

        Task<ProductFormServiceModel> FindById(int id);

        Task<bool> IsExist(int id);
    }
}
