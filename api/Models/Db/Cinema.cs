using System;
using System.Collections.Generic;

namespace api.Models
{
    public class Cinema
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CityId { get; set; }
        
        public virtual ICollection<CinemaMovie> CinemaMovies { get; set; }
        public virtual ICollection<Hall> Halls { get; set; }
    }
}