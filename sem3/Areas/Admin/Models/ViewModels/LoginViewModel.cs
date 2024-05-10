using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sem3.Areas.Admin.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required, RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be a string of 10 digits.")]
        public string Phone { get; set; }

        [Required, MinLength(8, ErrorMessage = "Password must be at least 8 characters long."), RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Password should only contain letters and numbers.")]
        public string Password { get; set; }
    }
}
