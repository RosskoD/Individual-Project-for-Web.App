using GobelinsWorld.Common.Mapping;
using GobelinsWorld.Data.Models;

namespace GobelinsWorld.Services.Admin.Models
{
    public class ProducerBrandListingServiceModel : IMapFrom<Producer>
    {
        public int Id { get; set; }

        public string LogoUrl { get; set; }
    }
}
