using System;
using System.Collections.Generic;

namespace api.Models
{
    public class Seat
    {
        public long Id { get; set; }
        public long HallId { get; set; }
        public string SeatType { get; set; }
        public int Row { get; set; }
        public int Position { get; set; }

        public virtual Hall Hall { get; set; }
        public virtual SeatType SeatTypeNavigation { get; set; }
    }
}