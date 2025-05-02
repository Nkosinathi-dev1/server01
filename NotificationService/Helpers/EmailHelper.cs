namespace NotificationService.Helpers
{
    public static class EmailHelper
    {
        public static Task SendEmailAsync(string to, string subject, string body)
        {
            Console.WriteLine($"Sending Email to: {to}\\nSubject: {subject}\\nBody: {body}");
            return Task.CompletedTask;
        }
    }
}
