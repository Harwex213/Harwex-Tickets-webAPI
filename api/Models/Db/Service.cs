using System.Collections.Generic;
using api.Models.Abstract;

namespace api.Models
{
    public class Service : BaseEntity
    {
        public Service()
        {
            SessionServices = new List<SessionService>();
        }
        
        public string Name { get; set; }
        
        public virtual ICollection<SessionService> SessionServices { get; set; }
    }
}