using SharedKernel.Entities;

namespace VisitorService.Models
{
    public class Visitor : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public ICollection<VisitLog> VisitLogs { get; set; } = new List<VisitLog>();
    }
}
