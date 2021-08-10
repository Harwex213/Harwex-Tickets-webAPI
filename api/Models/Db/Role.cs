using System.Collections.Generic;

namespace api.Models
{
    public class Role
    {
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}