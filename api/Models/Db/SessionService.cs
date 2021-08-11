using System;
using System.Collections.Generic;

namespace api.Models
{
    public class SessionService
    {
        public long Id { get; set; }
        public long SessionId { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }

        public virtual Session Session { get; set; }
        public virtual Service Service { get; set; }
    }
}