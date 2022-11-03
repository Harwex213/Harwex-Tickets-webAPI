using System.Collections.Generic;
using Domain.Base;

namespace Domain.Entities
{
    public class Hall : EntityBase<long>
    {
        public Hall()
        {
            Seats = new HashSet<Seat>();
            Sessions = new HashSet<Session>();
        }
        
        public long CinemaId { get; set; }
        public short RowsAmount { get; set; }
        public short ColsAmount { get; set; }
        
        public virtual Cinema Cinema { get; set; }
        public virtual ICollection<Seat> Seats { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}