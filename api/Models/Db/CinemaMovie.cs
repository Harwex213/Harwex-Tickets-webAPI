using System;
using System.Collections.Generic;

namespace api.Models
{
    public class CinemaMovie
    {
        public long Id { get; set; }
        public long CinemaId { get; set; }
        public long MovieId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public virtual Cinema Cinema { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}