namespace Service.Models.Ticket
{
    public class UpdateTicketModel
    {
        public long Id { get; set; }
        public long SessionId { get; set; }
        public long SeatId { get; set; }
        public long UserId { get; set; }
    }
}