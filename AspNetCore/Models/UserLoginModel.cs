using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage ="Enter User Name")]
        public string  UserName { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        public string  Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
