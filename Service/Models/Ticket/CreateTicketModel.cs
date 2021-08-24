namespace Service.Models.Ticket
{
    public class CreateTicketModel
    {
        public long SessionId { get; set; }
        public long SeatId { get; set; }
        public long UserId { get; set; }
    }

    public class CreateTicketResponseModel : BaseResponseModel
    {
    }
}