using System.Collections.Generic;
using api.Models.Abstract;

namespace api.Models
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
    }
}