namespace GobelinsWorld.Services.Admin.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class ProducerFormServiceModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ProducerNameMaxLength)]
        public string Name { get; set; }

        public string LogoUrl { get; set; }       
    }
}
