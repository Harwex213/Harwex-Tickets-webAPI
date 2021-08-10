using System;

namespace api.Models
{
    public class SessionSeatPrice
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public string SeatType { get; set; }
        public decimal Price { get; set; }
        
        public virtual Session Session { get; set; }
        public virtual SeatType SeatTypeNavigation { get; set; }
    }
}