using System;

namespace api.Models
{
    public class Session
    {
        public Guid Id { get; set; }
        public Guid HallId { get; set; }
        public Guid CinemaMovieId { get; set; }
        public DateTime Time { get; set; }
        
        public virtual Hall Hall { get; set; }
        public virtual CinemaMovie CinemaMovie { get; set; }
    }
}