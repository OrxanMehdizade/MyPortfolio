namespace MyPortfolio.Models
{
    public class ContactMessage
    {
        static int _staticId = 0;
        public int Id { get; set; } = ++_staticId;
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string contactMessage { get; set; }
    }
}
