using System;
using System.Collections.Generic;
using Domain.Base;

namespace Domain.Entities
{
    public class Session : EntityBase<long>
    {
        public Session()
        {
            SessionSeatPrices = new HashSet<SessionSeatPrice>();
        }
        
        public long HallId { get; set; }
        public long MovieId { get; set; }
        public DateTime Time { get; set; }
        
        public virtual Hall Hall { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual ICollection<SessionSeatPrice> SessionSeatPrices { get; set; }
    }
}