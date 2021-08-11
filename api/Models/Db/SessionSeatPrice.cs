using System;
using System.Collections.Generic;

namespace api.Models
{
    public class SessionSeatPrice
    {
        public long Id { get; set; }
        public long SessionId { get; set; }
        public string SeatType { get; set; }
        public decimal Price { get; set; }
        
        public virtual Session Session { get; set; }
        public virtual SeatType SeatTypeNavigation { get; set; }
    }
}