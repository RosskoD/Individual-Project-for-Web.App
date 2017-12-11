namespace GobelinsWorld.Services.Admin.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class ProductListingServiceModel : IMapFrom<Product>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public double Weight { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Producer { get; set; }

        public string Category { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
               .CreateMap<Product, ProductListingServiceModel>()
               .ForMember(c => c.Producer, cfg => cfg.MapFrom(c => c.Producer.Name))
               .ForMember(c => c.Category, cfg => cfg.MapFrom(c => c.Category.Name));
        }
    }
}
