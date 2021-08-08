using System.ComponentModel.DataAnnotations;

namespace api.Models.Requests
{
    public class RefreshRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}