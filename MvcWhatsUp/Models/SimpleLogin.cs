namespace MvcWhatsUp.Models
{
    public class SimpleLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public SimpleLogin()
        {
            Email = "";
            Password = "";
        }
        public SimpleLogin(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
