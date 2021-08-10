using System;
using System.Collections.Generic;

namespace api.Models
{
    public class CinemaMovie
    {
        public Guid Id { get; set; }
        public Guid CinemaId { get; set; }
        public Guid MovieId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public virtual Cinema Cinema { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}