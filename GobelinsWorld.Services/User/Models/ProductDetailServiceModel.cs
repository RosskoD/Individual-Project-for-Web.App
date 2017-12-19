namespace GobelinsWorld.Services.User.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class ProductDetailServiceModel : IMapFrom<Product>, IHaveCustomMapping
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Code { get; set; }

        [Range(0, double.MaxValue)]
        public double Weight { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        
        public string Description { get; set; }
        
        public string ImageUrl { get; set; }

        [Display(Name = "Producer")]
        public string ProducerName { get; set; }

        [Range(0,int.MaxValue)]
        public int Quantity { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<Product, ProductDetailServiceModel>()
                .ForMember(p => p.ProducerName, cfg => cfg.MapFrom(p => p.Producer.Name));
        }
    }
}
