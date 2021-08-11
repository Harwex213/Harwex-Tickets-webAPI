using System;
using System.Collections.Generic;
using api.Models.Abstract;

namespace api.Models
{
    public class Session : BaseEntity
    {
        public long HallId { get; set; }
        public DateTime Time { get; set; }
        
        public virtual Hall Hall { get; set; }
        public virtual ICollection<SessionSeatPrice> SessionSeatPrices { get; set; }
    }
}