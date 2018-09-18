﻿using System.Collections.Generic;

namespace Movies4U.Models
{
    public class Language
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // Many 2 Many with Movie
        public ICollection<MovieLanguages> MovieLanguages { get; set; }
    }
}
