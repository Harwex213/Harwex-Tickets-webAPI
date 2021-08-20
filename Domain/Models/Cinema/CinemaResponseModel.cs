using System.Collections.Generic;
using Domain.Models.Hall;

namespace Domain.Models.Cinema
{
    public class CinemaResponseModel : BaseResponseModel
    {
        public string Name { get; set; }
        public long CityId { get; set; }
        public ICollection<HallModel> Halls { get; set; }
    }
}