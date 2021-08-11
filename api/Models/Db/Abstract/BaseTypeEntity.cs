using System;
using System.ComponentModel.DataAnnotations;

namespace api.Models.Abstract
{
    public class BaseTypeEntity
    {
        [Key]
        public string Name { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}