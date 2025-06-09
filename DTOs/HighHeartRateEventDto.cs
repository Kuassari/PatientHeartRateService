namespace PatientHeartRateService.DTOs
{
    public class HighHeartRateEventDto
    {
        public string PatientId { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public int HeartRate { get; set; }
    }
}
