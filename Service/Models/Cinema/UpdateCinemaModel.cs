using System.Collections.Generic;
using Service.Models.Hall;

namespace Service.Models.Cinema
{
    public class UpdateCinemaModel : ICinemaGeneratableModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CityId { get; set; }
        public ICollection<HallModel> Halls { get; set; }
    }
}