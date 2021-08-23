namespace Service.Models.Hall
{
    public class CreateHallResponseModel
    {
        public long Id { get; set; }
    }
    public class HallModel
    {
        public long Id { get; set; }
        public short RowsAmount { get; set; }
        public short ColsAmount { get; set; }
    }
}