using System;
using System.Collections.Generic;
using api.Models.Abstract;

namespace api.Models
{
    public class Cinema : BaseEntity
    {
        public Cinema()
        {
            CinemaMovies = new List<CinemaMovie>();
            Halls = new List<Hall>();
        }
        
        public string Name { get; set; }
        public long CityId { get; set; }
        
        public virtual City City { get; set; }
        public virtual ICollection<CinemaMovie> CinemaMovies { get; set; }
        public virtual ICollection<Hall> Halls { get; set; }
    }
}