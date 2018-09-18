using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movies4U.Models
{
    public class MovieGenres
    {
        [Display(Name = "Movie")]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }

    }
}
