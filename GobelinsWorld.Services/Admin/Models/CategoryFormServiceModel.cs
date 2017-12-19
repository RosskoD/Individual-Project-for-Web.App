namespace GobelinsWorld.Services.Admin.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

   public class CategoryFormServiceModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }
    }
}
