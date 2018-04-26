using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OAuth2.Ouath2.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        public string Email { get; set; }


        public string Password { get; set; }


        public string ConfirmPassword { get; set; }

    }
}