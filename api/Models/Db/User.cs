namespace api.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        
        public virtual Role UserRoleNavigation { get; set; }
    }
}