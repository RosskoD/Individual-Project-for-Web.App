namespace GobelinsWorld.Web.Areas.Admin.Models.Categories
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class CategoryFormViewModel
    {
        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }
    }
}
