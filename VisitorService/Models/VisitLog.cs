using SharedKernel.Entities;

namespace VisitorService.Models
{
    public class VisitLog : BaseEntity
    {
        public int VisitorId { get; set; }
        public Visitor Visitor { get; set; } = null!;
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string Purpose { get; set; } = string.Empty;
    }
}
