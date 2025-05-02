using VisitorService.Dtos;
using VisitorService.Services.Interfaces;

namespace VisitorService.Endpoints
{
    public static class VisitorEndpoints
    {
        public static void MapVisitorEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/visitors/checkin", async (VisitorCheckInDto dto, IVisitorService service) =>
            {
                var response = await service.CheckInAsync(dto);
                return Results.Ok(response);
            });

            app.MapPost("/api/visitors/checkout", async (VisitorCheckOutDto dto, IVisitorService service) =>
            {
                var response = await service.CheckOutAsync(dto);
                return response ? Results.Ok() : Results.BadRequest("Invalid visit log ID or already checked out.");
            });
        }
    }
}
