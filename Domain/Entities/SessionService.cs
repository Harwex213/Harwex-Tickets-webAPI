using System.Collections.Generic;
using Domain.Base;

namespace Domain.Entities
{
    public class SessionService : EntityBase<long>
    {
        public SessionService()
        {
            Tickets = new HashSet<Ticket>();
        }
        
        public long SessionId { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }

        public virtual Session Session { get; set; }
        public virtual Service Service { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}