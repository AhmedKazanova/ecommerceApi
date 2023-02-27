using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Configurations
{
   public class UserRegisterationDto
    {
       
        public string UserName { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; }

        public string Role { get; set; }

    }
}
