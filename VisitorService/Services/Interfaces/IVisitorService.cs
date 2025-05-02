using VisitorService.Dtos;

namespace VisitorService.Services.Interfaces
{
    public interface IVisitorService
    {
        Task<VisitorResponseDto> CheckInAsync(VisitorCheckInDto dto);
        Task<bool> CheckOutAsync(VisitorCheckOutDto dto);
    }
}
