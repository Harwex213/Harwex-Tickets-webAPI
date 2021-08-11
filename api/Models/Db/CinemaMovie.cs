using System;
using System.Collections.Generic;
using api.Models.Abstract;

namespace api.Models
{
    public class CinemaMovie : BaseEntity
    {
        public CinemaMovie()
        {
            Sessions = new List<Session>();
        }
        
        public long CinemaId { get; set; }
        public long MovieId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public virtual Cinema Cinema { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}