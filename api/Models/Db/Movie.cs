using System;
using System.Collections.Generic;

namespace api.Models
{
    public class Movie
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public string Genre { get; set; }
        
        public virtual ICollection<CinemaMovie> CinemaMovies { get; set; }
    }
}