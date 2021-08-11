using System;
using System.ComponentModel.DataAnnotations;

namespace api.Models.Abstract
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}