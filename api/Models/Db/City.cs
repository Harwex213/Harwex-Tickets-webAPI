using System;
using System.Collections.Generic;
using api.Models.Abstract;

namespace api.Models
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public string Region { get; set; }
        
        public virtual ICollection<Cinema> Cinemas { get; set; }
    }
}