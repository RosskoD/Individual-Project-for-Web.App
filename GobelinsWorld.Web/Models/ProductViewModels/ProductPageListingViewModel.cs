namespace GobelinsWorld.Web.Models.ProductViewModels
{   
    using GobelinsWorld.Services.User.Models;
    using System.Collections.Generic;

    public class ProductPageListingViewModel : HomeIndexViewModel
    {
        public IEnumerable<UserProductListingServiceModel> Products { get; set; }

        public int CurrentPage { get; set; }

        public int TotalProducts { get; set; }

        public int TotalPages { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;
    }
}
