using PatientHeartRateService.DTOs;

namespace PatientHeartRateService.Services
{
    public interface IHeartRateService
    {
        Task<IEnumerable<HighHeartRateEventDto>> GetHighHeartRateEventsAsync();
        Task<HeartRateAnalyticsDto?> GetHeartRateAnalyticsAsync(string patientId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<HeartRateAnalyticsDto>> GetAllPatientsAnalyticsAsync(DateTime startDate, DateTime endDate);
    }
}
