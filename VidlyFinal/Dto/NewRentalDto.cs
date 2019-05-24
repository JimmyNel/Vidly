using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VidlyExercice1.Dto
{
    public class NewRentalDto
    {
        public int CustomerId  { get; set; }
        public List<int> MoviesIds { get; set; }
    }
}