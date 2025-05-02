using VisitorService.Models;

namespace VisitorService.Repositories.Interfaces
{
    public interface IVisitorRepository
    {
        Task<Visitor> GetOrCreateVisitorAsync(string email, string fullName, string phoneNumber);
        Task<VisitLog> CheckInVisitorAsync(Visitor visitor, string purpose);
        Task<VisitLog?> CheckOutVisitorAsync(int visitLogId);
    }
}
