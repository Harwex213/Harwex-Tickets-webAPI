using System.Collections.Generic;
using Domain.Base;

namespace Domain.Entities
{
    public class Role : EntityBase<long>
    {
        public Role()
        {
            Users = new HashSet<User>();
        }
        
        public string Name { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
    }
}