using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using api.Models.Abstract;

namespace api.Models
{
    public class SessionSeatPrice : BaseEntity
    {
        public SessionSeatPrice()
        {
            Tickets = new List<Ticket>();
        }
        
        public long SessionId { get; set; }
        public string SeatTypeName { get; set; }
        public decimal Price { get; set; }
        
        public virtual Session Session { get; set; }
        public virtual SeatType SeatType { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}