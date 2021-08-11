using System;
using System.Collections.Generic;
using api.Models.Abstract;

namespace api.Models
{
    public class Movie : BaseEntity
    {
        public Movie()
        {
            CinemaMovies = new List<CinemaMovie>();
        }
        
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public string Genre { get; set; }

        public virtual ICollection<CinemaMovie> CinemaMovies { get; set; }
    }
}