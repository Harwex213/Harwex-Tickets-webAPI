using Domain.Base;

namespace Domain.Entities
{
    public class Ticket : EntityBase<long>
    {
        public long SessionSeatPriceId { get; set; }
        public long SeatId { get; set; }
        public long UserId { get; set; }

        public virtual SessionSeatPrice SessionSeatPrice { get; set; }
        public virtual Seat Seat { get; set; }
        public virtual User User { get; set; }
    }
}