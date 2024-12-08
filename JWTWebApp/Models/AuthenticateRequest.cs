namespace JWTWebApp.Models
{
    public class AuthenticateRequest
    {
        public string Email {  get; set; }
        public string Password { get; set; }
        public string Role {  get; set; }
    }
}
