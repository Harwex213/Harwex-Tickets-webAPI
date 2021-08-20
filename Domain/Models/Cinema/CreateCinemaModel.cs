using System.Collections.Generic;
using Domain.Models.Hall;

namespace Domain.Models.Cinema
{
    public class CreateCinemaModel
    {
        public string Name { get; set; }
        public long CityId { get; set; }
        public ICollection<HallModel> Halls { get; set; }
    }

    public class CreateCinemaResponseModel : BaseResponseModel
    {
    }
}