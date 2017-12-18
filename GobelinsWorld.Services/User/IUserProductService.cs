namespace GobelinsWorld.Services.User
{
    using Admin.Models;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserProductService
    {
        Task<IEnumerable<UserProductListingServiceModel>> New();

        Task<IEnumerable<UserProductListingServiceModel>> AllByCategory(int id, int page, int pageSize);

        Task<IEnumerable<UserProductListingServiceModel>> AllByProducer(int id, int page, int pageSize);

        Task<IEnumerable<UserProductListingServiceModel>> AllBySearch(string searchText, int page, int pageSize);

        Task<ProductDetailServiceModel> Detail(int id);

        int Total();
    }
}
