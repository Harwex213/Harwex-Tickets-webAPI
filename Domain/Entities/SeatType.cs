using System.Collections.Generic;
using Domain.Base;

namespace Domain.Entities
{
    public class SeatType : EntityBase<long>
    {
        public SeatType()
        {
            Seats = new HashSet<Seat>();
            SessionSeatPrices = new List<SessionSeatPrice>();
        }
        
        public string Name { get; set; }
        
        public virtual ICollection<Seat> Seats { get; set; }
        public virtual ICollection<SessionSeatPrice> SessionSeatPrices { get; set; }
    }
}