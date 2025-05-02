using AuthService.Dtos;
using AuthService.Services.Interfaces;

namespace AuthService.Endpoints
{
    public static class AuthEndpoints
    {

        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/auth/register", async (RegisterDto dto, IAuthService service) =>
            {
                var result = await service.RegisterAsync(dto);
                return Results.Ok(result);
            });

            app.MapPost("/api/auth/login", async (LoginDto dto, IAuthService service) =>
            {
                var result = await service.LoginAsync(dto);
                return Results.Ok(result);
            });

            app.MapPost("/api/auth/refresh", async (HttpContext context, IAuthService service) =>
            {
                var body = await context.Request.ReadFromJsonAsync<Dictionary<string, string>>();
                if (body == null || !body.TryGetValue("refreshToken", out var token))
                    return Results.BadRequest("Missing refresh token");

                var result = await service.RefreshTokenAsync(token);
                return Results.Ok(result);
            });
        }
    }
}
