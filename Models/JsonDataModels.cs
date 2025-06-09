namespace PatientHeartRateService.Models
{
    public class JsonDataRoot
    {
        public List<JsonPatient> Patients { get; set; } = new();
        public List<JsonHeartRateReading> HeartRateReadings { get; set; } = new();
    }

    public class JsonPatient
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
    }

    public class JsonHeartRateReading
    {
        public string PatientId { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public int HeartRate { get; set; }
    }
}
