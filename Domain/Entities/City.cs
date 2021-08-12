using System.Collections.Generic;
using Domain.Base;

namespace Domain.Entities
{
    public class City : EntityBase<long>
    {
        public City()
        {
            Cinemas = new HashSet<Cinema>();
        }
        
        public string Name { get; set; }
        public string Region { get; set; }
        
        public virtual ICollection<Cinema> Cinemas { get; set; }
    }
}