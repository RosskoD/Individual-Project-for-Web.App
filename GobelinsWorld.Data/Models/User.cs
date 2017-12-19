namespace GobelinsWorld.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class User : IdentityUser
    {
        [Required]
        [MaxLength(UserNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(UserNameMaxLength)]
        public string LastName { get; set; }

        public Country Country { get; set; }

        [Required]
        [MaxLength(UserStateMaxLength)]
        public string State { get; set; }

        [Required]
        [MaxLength(UserCityMaxLength)]
        public string City { get; set; }

        [Required]
        [MaxLength(UserZipCodeMaxLength)]
        public string ZipCode { get; set; }

        [Required]
        [MaxLength(UserStreetAddressMaxLength)]
        public string StreetAddress { get; set; }

        public bool IsPersonalAccount { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
