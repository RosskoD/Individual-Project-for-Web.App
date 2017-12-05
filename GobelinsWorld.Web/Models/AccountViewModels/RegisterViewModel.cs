﻿namespace GobelinsWorld.Web.Models.AccountViewModels
{
    using Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
       [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
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
        [Display(Name ="Street Address")]
        public string StreetAddress { get; set; }

        [Display(Name = "Account")]
        public bool IsPersonalAccount { get; set; } = true;
    }

}
