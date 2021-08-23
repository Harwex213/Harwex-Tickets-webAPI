using System.ComponentModel.DataAnnotations;

namespace Domain.Base
{
    public interface IEntityBase
    {
    }

    public abstract class EntityBase<TKey> : IEntityBase
    {
        [Key]
        public virtual TKey Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}