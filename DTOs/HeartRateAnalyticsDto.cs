namespace PatientHeartRateService.DTOs
{
    public class HeartRateAnalyticsDto
    {
        public string PatientId { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public double AverageHeartRate { get; set; }
        public int MaxHeartRate { get; set; }
        public int MinHeartRate { get; set; }
        public int ReadingsCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
