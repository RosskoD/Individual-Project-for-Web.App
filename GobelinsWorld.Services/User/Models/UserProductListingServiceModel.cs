namespace GobelinsWorld.Services.User.Models
{
    using Common.Mapping;
    using Data.Models;

    public class UserProductListingServiceModel :IMapFrom<Product>
    { 
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
    }
}
