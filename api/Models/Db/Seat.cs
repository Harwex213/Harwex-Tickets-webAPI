using System.ComponentModel.DataAnnotations.Schema;
using api.Models.Abstract;

namespace api.Models
{
    public class Seat : BaseEntity
    {
        public long HallId { get; set; }
        public string SeatType { get; set; }
        public int Row { get; set; }
        public int Position { get; set; }

        public virtual Hall Hall { get; set; }

        [ForeignKey("SeatType")] public virtual SeatType SeatTypeNavigation { get; set; }
    }
}