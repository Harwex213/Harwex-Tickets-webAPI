﻿using System;
using System.Collections.Generic;
using api.Models.Abstract;

namespace api.Models
{
    public class User : BaseEntity
    {
        public User()
        {
            Tickets = new List<Ticket>();
        }
        
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string RoleName { get; set; }
        
        public virtual Role Role { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}