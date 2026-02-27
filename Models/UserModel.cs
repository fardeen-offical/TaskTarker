namespace TaskTracker.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // Real project mein hash use karte hain
        public string Role { get; set; }
    }
}