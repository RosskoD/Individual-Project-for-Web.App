namespace GobelinsWorld.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using static DataConstants;

    public class Product
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

        public int ProducerId { get; set; }

        public Producer Producer { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
