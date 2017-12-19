namespace GobelinsWorld.Services.Admin.Models
{
    using Common.Mapping;
    using Data.Models;

    public class ProducerBrandListingServiceModel : IMapFrom<Producer>
    {
        public int Id { get; set; }

        public string LogoUrl { get; set; }
    }
}
