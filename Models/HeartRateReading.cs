using System.ComponentModel.DataAnnotations;

namespace PatientHeartRateService.Models
{
    public class HeartRateReading
    {
        public int Id { get; set; }  

        [Required]
        public string PatientId { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; }

        [Range(30, 300)]
        public int HeartRate { get; set; }

        public Patient Patient { get; set; } = null!;
    }
}
