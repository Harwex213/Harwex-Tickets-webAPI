using System.Collections.Generic;
using api.Models.Abstract;

namespace api.Models
{
    public class SessionService : BaseEntity
    {
        public SessionService()
        {
            Tickets = new List<Ticket>();
        }
        
        public long SessionId { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }

        public virtual Session Session { get; set; }
        public virtual Service Service { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}