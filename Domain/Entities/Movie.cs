using System;
using System.Collections.Generic;
using Domain.Base;

namespace Domain.Entities
{
    public class Movie : EntityBase<long>
    {
        public Movie()
        {
            CinemaMovies = new HashSet<CinemaMovie>();
        }
        
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public string Genre { get; set; }

        public virtual ICollection<CinemaMovie> CinemaMovies { get; set; }
    }
}