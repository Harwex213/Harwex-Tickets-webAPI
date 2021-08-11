using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using api.Models.Abstract;

namespace api.Models
{
    public class Seat : BaseEntity
    {
        public Seat()
        {
            Tickets = new List<Ticket>();
        }
        
        public long HallId { get; set; }
        public string SeatTypeName { get; set; }
        public int Row { get; set; }
        public int Position { get; set; }

        public virtual Hall Hall { get; set; } 
        public virtual SeatType SeatType { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}