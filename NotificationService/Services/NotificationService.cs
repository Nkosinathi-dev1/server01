using NotificationService.Helpers;
using NotificationService.Models;
using NotificationService.Services.Interfaces;

namespace NotificationService.Services
{
    public class NotificationService : INotificationService
    {
        public Task HandleNotificationAsync(NotificationMessage message)
        {
            return message.Type.ToLower() switch
            {
                "sms" => SmsHelper.SendSmsAsync(message.To, message.Body),
                _ => EmailHelper.SendEmailAsync(message.To, message.Subject, message.Body)
            };
        }
    }
}
