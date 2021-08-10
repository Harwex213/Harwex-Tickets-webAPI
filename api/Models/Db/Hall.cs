using System;
using System.Collections.Generic;

namespace api.Models
{
    public class Hall
    {
        public Guid Id { get; set; }
        public Guid CinemaId { get; set; }
        
        public virtual Cinema Cinema { get; set; }
        public virtual ICollection<Seat> Seats { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}