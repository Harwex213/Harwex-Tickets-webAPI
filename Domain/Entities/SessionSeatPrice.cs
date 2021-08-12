using System.Collections.Generic;
using Domain.Base;

namespace Domain.Entities
{
    public class SessionSeatPrice : EntityBase<long>
    {
        public SessionSeatPrice()
        {
            Tickets = new HashSet<Ticket>();
        }
        
        public long SessionId { get; set; }
        public string SeatTypeName { get; set; }
        public decimal Price { get; set; }
        
        public virtual Session Session { get; set; }
        public virtual SeatType SeatType { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}