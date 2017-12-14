namespace GobelinsWorld.Web.Models.ProductViewModels
{
    using Common.Mapping;
    using GobelinsWorld.Services.User.Models;

    public class ProductDetailViewModel : HomeIndexViewModel
    {
        public ProductDetailServiceModel Product { get; set; }
    }
}
