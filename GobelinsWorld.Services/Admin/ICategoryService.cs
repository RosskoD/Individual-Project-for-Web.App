namespace GobelinsWorld.Services.Admin
{
    using Data.Models;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
       Task<IList<CategoryFormServiceModel>> All();

        Task Create(string name);

        Task<CategoryFormServiceModel> FindById(int id);

        Task<bool> Exist(int id);

        Task Edit(int id, string name);

        Task Delete(int id);
    }
}
