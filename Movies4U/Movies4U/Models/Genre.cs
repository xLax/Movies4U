﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies4U.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // Many 2 Many with Movie
        public ICollection<MovieGenres> MovieGenres { get; set; }
    }
}
