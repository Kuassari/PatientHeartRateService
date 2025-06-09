using System.ComponentModel.DataAnnotations;

namespace PatientHeartRateService.Models
{
    public class Patient
    {
        public string Id { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty;

        public int RequestCount { get; set; } = 0;

        public ICollection<HeartRateReading> HeartRateReadings { get; set; } = new List<HeartRateReading>();
    }
}
