namespace NotificationService.Helpers
{
    public static class SmsHelper
    {
        public static Task SendSmsAsync(string to, string message)
        {
            Console.WriteLine($"Sending SMS to: {to}\\nMessage: {message}");
            return Task.CompletedTask;
        }
    }
}
