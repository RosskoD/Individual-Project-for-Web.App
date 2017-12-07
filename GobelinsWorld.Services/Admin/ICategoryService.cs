namespace GobelinsWorld.Services.Admin
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<IEnumerable<CategoryFormServiceModel>> All();

        Task Create(string name);

        Task Edit(int id, string name);

        Task Delete(int id);

        Task<CategoryFormServiceModel> FindById(int id);

        Task<bool> Exist(int id);
    }
}
