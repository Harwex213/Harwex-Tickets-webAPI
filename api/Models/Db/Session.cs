using System;
using System.Collections.Generic;

namespace api.Models
{
    public class Session
    {
        public Guid Id { get; set; }
        public Guid HallId { get; set; }
        public DateTime Time { get; set; }
        
        public virtual Hall Hall { get; set; }
        public virtual ICollection<SessionSeatPrice> SessionSeatPrices { get; set; }
    }
}