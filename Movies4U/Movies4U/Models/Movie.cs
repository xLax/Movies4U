using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies4U.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public int NumOfMinutes { get; set; }

        public string Summary { get; set; }

        public int MinimumAge { get; set; }

    }
}
