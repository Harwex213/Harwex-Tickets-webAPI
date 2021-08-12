using System.Collections.Generic;
using Domain.Base;

namespace Domain.Entities
{
    public class Service : EntityBase<long>
    {
        public Service()
        {
            SessionServices = new HashSet<SessionService>();
        }
        
        public string Name { get; set; }
        
        public virtual ICollection<SessionService> SessionServices { get; set; }
    }
}