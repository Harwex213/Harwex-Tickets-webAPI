﻿using System.Collections.Generic;
using Domain.Base;

namespace Domain.Entities
{
    public class Cinema : EntityBase<long>
    {
        public Cinema()
        {
            Halls = new HashSet<Hall>();
        }
        
        public string Name { get; set; }
        public long CityId { get; set; }
        
        public virtual City City { get; set; }
        public virtual ICollection<Hall> Halls { get; set; }
    }
}