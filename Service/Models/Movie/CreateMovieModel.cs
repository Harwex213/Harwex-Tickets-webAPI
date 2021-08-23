using System;

namespace Service.Models.Movie
{
    public class CreateMovieModel
    {
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public string Genre { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class CreateMovieResponseModel : BaseResponseModel
    {
    }
}