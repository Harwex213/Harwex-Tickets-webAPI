using System.Collections.Generic;
using Domain.Base;

namespace Domain.Entities
{
    public class User : EntityBase<long>
    {
        public User()
        {
            Tickets = new HashSet<Ticket>();
        }
        
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string RoleName { get; set; }
        
        public virtual Role Role { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}