namespace MvcWhatsUp.Models
{
    public class SimpleMessage
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public SimpleMessage()
        {
            Name = "";
            Message = "";
        }
        public SimpleMessage(string name, string message)
        {
            Name = name;
            Message = message;
        }
    }
}
