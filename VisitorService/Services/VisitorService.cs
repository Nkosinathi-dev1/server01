using VisitorService.Dtos;
using VisitorService.Repositories.Interfaces;
using VisitorService.Services.Interfaces;

namespace VisitorService.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly IVisitorRepository _repository;

        public VisitorService(IVisitorRepository repository)
        {
            _repository = repository;
        }
        public async Task<VisitorResponseDto> CheckInAsync(VisitorCheckInDto dto)
        {
            var visitor = await _repository.GetOrCreateVisitorAsync(dto.Email, dto.FullName, dto.PhoneNumber);
            var visitLog = await _repository.CheckInVisitorAsync(visitor, dto.Purpose);

            return new VisitorResponseDto
            {
                VisitorId = visitor.Id,
                VisitLogId = visitLog.Id,
                FullName = visitor.FullName,
                Email = visitor.Email,
                CheckInTime = visitLog.CheckInTime
            };
        }

        public async Task<bool> CheckOutAsync(VisitorCheckOutDto dto)
        {
            var result = await _repository.CheckOutVisitorAsync(dto.VisitLogId);
            return result != null;
        }
    }
}
