using System;
using System.Collections.Generic;

namespace api.Models
{
    public class Hall
    {
        public long Id { get; set; }
        public long CinemaId { get; set; }
        
        public virtual Cinema Cinema { get; set; }
        public virtual ICollection<Seat> Seats { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}