namespace GobelinsWorld.Web.Areas.Admin.Models.Products
{
    using GobelinsWorld.Common.Mapping;
    using GobelinsWorld.Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class ProductFormModel 
    {
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
        [Display(Name ="Image URL")]
        public string ImageUrl { get; set; }

        [Display(Name="Producer")]
        public int ProducerId { get; set; }

        public IEnumerable<SelectListItem> Producers { get; set; }

        [Display(Name="Category")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
