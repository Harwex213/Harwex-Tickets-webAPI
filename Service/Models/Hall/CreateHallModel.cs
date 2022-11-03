namespace Service.Models.Hall
{
    public class CreateHallModel
    {
        public long CinemaId { get; set; }
        public short RowsAmount { get; set; }
        public short ColsAmount { get; set; }
    }
    public class CreateHallResponseModel : BaseResponseModel
    {
    }
}