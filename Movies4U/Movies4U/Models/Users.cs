﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies4U.Models
{
    public class Users
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime Birthdate { get; set; } 
    }
}
