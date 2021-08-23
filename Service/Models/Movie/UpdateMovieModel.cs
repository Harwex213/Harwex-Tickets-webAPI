using System;

namespace Service.Models.Movie
{
    public class UpdateMovieModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public string Genre { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}