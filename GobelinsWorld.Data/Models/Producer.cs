namespace GobelinsWorld.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Producer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ProducerNameMaxLength)]
        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
