using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assesment8.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }


        [Required]
        public string Password { get; set; }
    }
}