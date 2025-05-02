namespace NotificationService.Models
{
    public class NotificationMessage
    {
        public string Type { get; set; } = "email"; // "email" or "sms"
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
