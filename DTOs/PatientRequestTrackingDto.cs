namespace PatientHeartRateService.DTOs
{
    public class PatientRequestTrackingDto
    {
        public string PatientId { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public int RequestCount { get; set; }
    }
}
