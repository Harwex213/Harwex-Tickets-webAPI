namespace Service.Models.Ticket
{
    public class TicketResponseModel : BaseResponseModel
    {
        public long SessionId { get; set; }
        public long SeatId { get; set; }
        public long UserId { get; set; }
    }
}