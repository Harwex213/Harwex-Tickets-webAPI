using api.Models.Abstract;

namespace api.Models
{
    public class Ticket : BaseEntity
    {
        public long SessionServiceId { get; set; }
        public long SessionSeatPriceId { get; set; }
        public long SeatId { get; set; }
        public long UserId { get; set; }

        public virtual SessionService SessionService { get; set; }
        public virtual SessionSeatPrice SessionSeatPrice { get; set; }
        public virtual Seat Seat { get; set; }
        public virtual User User { get; set; }
    }
}