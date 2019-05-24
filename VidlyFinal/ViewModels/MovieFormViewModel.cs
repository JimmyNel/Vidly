using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Razor;
using VidlyExercice1.ViewModels;

namespace VidlyExercice1.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<MovieGenre> Genres { get; set; }
        public Movie Movie { get; set; }
    }
}