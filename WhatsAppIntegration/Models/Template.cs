namespace WhatsAppIntegration.Models
{
    public class Template
    {
        public string name { get; set; }
        public Language language { get; set; }
        public List<WhatsAppComponent> components { get; set; }
    }
}