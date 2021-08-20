using System.Collections.Generic;
using Domain.Models.Hall;

namespace Domain.Models.Cinema
{
    public class UpdateCinemaModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CityId { get; set; }
        public ICollection<HallModel> Halls { get; set; }
    }
}