namespace GobelinsWorld.Web.Models
{
    using GobelinsWorld.Services.Admin.Models;
    using System.Collections.Generic;

    public class HomeIndexViewModel
    {
        public IEnumerable<CategoryFormServiceModel> Categories { get; set; }

        public IEnumerable<ProducerBrandListingServiceModel> Producers { get; set; }
    }
}
