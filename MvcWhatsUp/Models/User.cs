namespace MvcWhatsUp.Models
{
    public class User
    {
        public string EmailAddress { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string MobileNumber { get; set; }

        public User()
        {
            
        }
        public User(int id, string name, string mobileNumber, string emailAddress)
        {
            EmailAddress = emailAddress;
            UserId = id;
            UserName = name;
            MobileNumber = mobileNumber;
        }
    }
}
