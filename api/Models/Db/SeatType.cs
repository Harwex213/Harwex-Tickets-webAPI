using System.Collections.Generic;
using api.Models.Abstract;

namespace api.Models
{
    public class SeatType : BaseEntity
    {
        public string Name { get; set; }
        
        public virtual ICollection<Seat> Seats { get; set; }
        public virtual ICollection<SessionSeatPrice> SessionSeatPrices { get; set; }
    }
}