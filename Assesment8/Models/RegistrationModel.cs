using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assesment8.Models
{
    public class RegistrationModel
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password does not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime AttendanceDate { get; set; }
        [Required]
        public string PlusOne { get; set; }
        public  string FavoriteCharacter { get; set; }

        public enum choices
        {
            Eddard_Stark,
            Arya_Stark,
            tyrion_lannister,
            Jon_Snow,
            Joffrey_baratheon
        }

    }
}