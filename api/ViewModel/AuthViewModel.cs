using System.ComponentModel.DataAnnotations;

namespace api.ViewModel
{
    public class AuthLoginRequest
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
    }

    public class AuthRefreshRequest
    {
        [Required] public string RefreshToken { get; set; }
    }

    public class AuthRegisterRequest
    {
        [Required] public string Username { get; set; }
        [Required] public string PhoneNumber { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string ConfirmPassword { get; set; }
    }

    public class AuthAuthenticatedResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}