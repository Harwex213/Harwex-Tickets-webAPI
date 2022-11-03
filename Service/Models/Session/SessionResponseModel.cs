using System;

namespace Service.Models.Session
{
    public class SessionResponseModel : BaseResponseModel
    {
        public long HallId { get; set; }
        public long MovieId { get; set; }
        public DateTime Time { get; set; }
        public decimal Price { get; set; }
    }
}