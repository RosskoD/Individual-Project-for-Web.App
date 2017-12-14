namespace GobelinsWorld.Web.Models.ManageViewModels
{
    using Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class IndexViewModel : HomeIndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }               

        public string StatusMessage { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public Country Country { get; set; } = Country.Bulgaria;

        [Required]
        public string State { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [Display(Name = "Zip/Post Code")]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Display(Name = "Account")]
        public bool IsPersonalAccount { get; set; } = true;
    }
}
