using System.Collections.Generic;
using api.Models.Abstract;

namespace api.Models
{
    public class Role : BaseTypeEntity
    {
        public virtual ICollection<User> Users { get; set; }
    }
}