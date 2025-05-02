using Microsoft.EntityFrameworkCore;
using VisitorService.Data;
using VisitorService.Models;
using VisitorService.Repositories.Interfaces;

namespace VisitorService.Repositories
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly VisitorDbContext _context;

        public VisitorRepository(VisitorDbContext context)
        {
            _context = context;
        }

        public async Task<VisitLog> CheckInVisitorAsync(Visitor visitor, string purpose)
        {
            var log = new VisitLog
            {
                VisitorId = visitor.Id,
                CheckInTime = DateTime.UtcNow,
                Purpose = purpose
            };
            await _context.VisitLogs.AddAsync(log);
            await _context.SaveChangesAsync();
            return log;
        }

        public async Task<VisitLog?> CheckOutVisitorAsync(int visitLogId)
        {
            var log = await _context.VisitLogs.FindAsync(visitLogId);
            if (log == null || log.CheckOutTime != null) return null;

            log.CheckOutTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return log;
        }

        public async Task<Visitor> GetOrCreateVisitorAsync(string email, string fullName, string phoneNumber)
        {
            var visitor = await _context.Visitors.FirstOrDefaultAsync(v => v.Email == email);
            if (visitor == null)
            {
                visitor = new Visitor
                {
                    Email = email,
                    FullName = fullName,
                    PhoneNumber = phoneNumber
                };
                await _context.Visitors.AddAsync(visitor);
                await _context.SaveChangesAsync();
            }
            return visitor;
        }
    }
}
