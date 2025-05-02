using NotificationService.Models;
using NotificationService.Services.Interfaces;

namespace NotificationService.Endpoints
{
    public static class NotificationEndpoints
    {
        public static void MapNotificationEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/notify", async (NotificationMessage message, INotificationService service) =>
            {
                await service.HandleNotificationAsync(message);
                return Results.Ok("Notification sent.");
            });
        }
    }
}
