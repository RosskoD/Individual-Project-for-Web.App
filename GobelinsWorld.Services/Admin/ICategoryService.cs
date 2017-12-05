namespace GobelinsWorld.Services.Admin
{
    using Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
       Task<IList<Category>> All();
    }
}
