namespace VisitorService.Dtos
{
    public class VisitorResponseDto
    {
        public int VisitorId { get; set; }
        public int VisitLogId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CheckInTime { get; set; }
    }
}
