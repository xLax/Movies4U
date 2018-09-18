using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movies4U.Models
{
    public class Users
    {
        [Key]
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime Birthdate { get; set; } 
    }
}
