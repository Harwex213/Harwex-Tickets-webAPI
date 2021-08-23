using System;
using System.ComponentModel.DataAnnotations;

namespace Service.Models.Movie
{
    public class MovieResponseModel : BaseResponseModel
    {
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public string Genre { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}