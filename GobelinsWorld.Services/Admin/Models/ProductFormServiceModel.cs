﻿namespace GobelinsWorld.Services.Admin.Models
{
    using Common.Mapping;
    using Data.Models;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class ProductFormServiceModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ProductNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(ProductCodeMaxLength)]
        public string Code { get; set; }

        [Range(0, double.MaxValue)]
        public double Weight { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(ProductDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Display(Name="Producer")]
        public int ProducerId { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}
