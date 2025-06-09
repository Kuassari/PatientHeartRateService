using PatientHeartRateService.Models;

namespace PatientHeartRateService.Repositories
{
    public interface IHeartRateReadingRepository
    {
        Task<IEnumerable<HeartRateReading>> GetHighHeartRateEventsAsync(int threshold = 100);
        Task<IEnumerable<HeartRateReading>> GetReadingsByPatientAndDateRangeAsync(
            string patientId, DateTime startDate, DateTime endDate);
    }
}
