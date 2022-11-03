using System;
using System.Collections.Generic;
using Domain.Base;

namespace Domain.Entities
{
    public class Movie : EntityBase<long>
    {
        public Movie()
        {
            Sessions = new HashSet<Session>();
        }
        
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public string Genre { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
}