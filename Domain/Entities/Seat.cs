using System.Collections.Generic;
using Domain.Base;

namespace Domain.Entities
{
    public class Seat : EntityBase<long>
    {
        public Seat()
        {
            Tickets = new HashSet<Ticket>();
        }
        
        public long HallId { get; set; }
        public int Row { get; set; }
        public int Position { get; set; }

        public virtual Hall Hall { get; set; } 
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}