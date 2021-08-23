using System;

namespace Service.Models.Session
{
    public class UpdateSessionModel
    {
        public long Id { get; set; }
        public long HallId { get; set; }
        public long MovieId { get; set; }
        public DateTime Time { get; set; }
        public decimal Price { get; set; }
    }
}