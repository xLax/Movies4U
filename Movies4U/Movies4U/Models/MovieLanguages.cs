﻿using System.ComponentModel.DataAnnotations;

namespace Movies4U.Models
{
    public class MovieLanguages
    {
        [Display(Name = "Movie")]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        public Language Language { get; set; }

    }
}
