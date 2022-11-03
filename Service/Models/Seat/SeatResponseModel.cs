namespace Service.Models.Seat
{
    public class SeatResponseModel : BaseResponseModel
    {
        public long HallId { get; set; }
        public int Row { get; set; }
        public int Position { get; set; }
    }
}