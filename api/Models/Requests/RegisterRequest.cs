using System.ComponentModel.DataAnnotations;

namespace api.Models.Requests
{
    public class RegisterRequest
    {
        [Required]
        public string Username { get; set; }
        
        [Required] 
        public string PhoneNumber { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}