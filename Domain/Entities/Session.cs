using System;
using System.Collections.Generic;
using Domain.Base;

namespace Domain.Entities
{
    public class Session : EntityBase<long>
    {
        public Session()
        {
            SessionServices = new HashSet<SessionService>();
            SessionSeatPrices = new HashSet<SessionSeatPrice>();
        }
        
        public long HallId { get; set; }
        public DateTime Time { get; set; }
        
        public virtual Hall Hall { get; set; }
        public virtual ICollection<SessionSeatPrice> SessionSeatPrices { get; set; }
        public virtual ICollection<SessionService> SessionServices { get; set; }
    }
}