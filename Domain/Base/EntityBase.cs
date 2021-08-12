using System.ComponentModel.DataAnnotations;

namespace Domain.Base
{
    public abstract class EntityBase<TKey>
    {
        [Key]
        public virtual TKey Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}