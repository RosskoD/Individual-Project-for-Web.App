namespace GobelinsWorld.Services.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class ProductByCategoryListingServiceModel :IMapFrom<Product>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<Product, ProductByCategoryListingServiceModel>()
                .ForMember(p => p.CategoryName, cfg => cfg.MapFrom(c => c.Category.Name));
        }
    }
}
