using api.Models.Abstract;

namespace api.Models
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public long UserId { get; set; }
    }
}