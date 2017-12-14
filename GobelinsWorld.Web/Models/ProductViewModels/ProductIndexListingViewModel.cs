namespace GobelinsWorld.Web.Models.ProductViewModels
{
    using GobelinsWorld.Services.User.Models;
    using System.Collections.Generic;

    public class ProductIndexListingViewModel : HomeIndexViewModel
    {
        public IEnumerable<UserProductListingServiceModel> Products { get; set; }
    }
}
