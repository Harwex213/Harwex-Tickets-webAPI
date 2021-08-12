using Domain.Base;

namespace Domain.Entities
{
    public class RefreshToken : EntityBase<long>
    {
        public string Token { get; set; }
        public long UserId { get; set; }
    }
}