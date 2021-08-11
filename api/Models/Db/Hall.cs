using System;
using System.Collections.Generic;
using api.Models.Abstract;

namespace api.Models
{
    public class Hall : BaseEntity
    {
        public long CinemaId { get; set; }
        
        public virtual Cinema Cinema { get; set; }
        public virtual ICollection<Seat> Seats { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}