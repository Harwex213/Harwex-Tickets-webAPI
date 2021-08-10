using System;

namespace api.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public long UserId { get; set; }
        public Guid SessionSeatPriceId { get; set; }
        public Guid SessionServiceId { get; set; }

        public virtual User User { get; set; }
        public virtual SessionSeatPrice SessionSeatPrice { get; set; }
        public virtual SessionService SessionService { get; set; }
    }
}