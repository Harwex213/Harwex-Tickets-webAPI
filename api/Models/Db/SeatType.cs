using System.Collections.Generic;

namespace api.Models
{
    public class SeatType
    {
        public string Name { get; set; }
        
        public virtual ICollection<Seat> Seats { get; set; }
        public virtual ICollection<SessionSeatPrice> SessionSeatPrices { get; set; }
    }
}