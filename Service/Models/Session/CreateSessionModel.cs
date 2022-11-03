using System;

namespace Service.Models.Session
{
    public class CreateSessionModel
    {
        public long HallId { get; set; }
        public long MovieId { get; set; }
        public DateTime Time { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateSessionResponseModel : BaseResponseModel
    {
    }
}