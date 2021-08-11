using System.Collections.Generic;
using api.Models.Abstract;

namespace api.Models
{
    public class SeatType : BaseTypeEntity
    {
        public virtual ICollection<Seat> Seats { get; set; }
        public virtual ICollection<SessionSeatPrice> SessionSeatPrices { get; set; }
    }
}