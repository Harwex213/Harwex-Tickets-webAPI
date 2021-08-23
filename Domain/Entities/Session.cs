using System;
using System.Collections.Generic;
using Domain.Base;

namespace Domain.Entities
{
    public class Session : EntityBase<long>
    {
        public Session()
        {
            Tickets = new HashSet<Ticket>();
        }
        
        public long HallId { get; set; }
        public long MovieId { get; set; }
        public DateTime Time { get; set; }
        public decimal Price { get; set; }
        
        public virtual Hall Hall { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}