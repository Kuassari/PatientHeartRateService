using Microsoft.EntityFrameworkCore;
using PatientHeartRateService.Data;
using PatientHeartRateService.Models;

namespace PatientHeartRateService.Repositories
{
    public class HeartRateReadingRepository : IHeartRateReadingRepository
    {
        private readonly HeartRateContext _context;

        public HeartRateReadingRepository(HeartRateContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HeartRateReading>> GetHighHeartRateEventsAsync(int threshold = 100)
        {
            return await _context.HeartRateReadings
                .Include(hr => hr.Patient)
                .Where(hr => hr.HeartRate > threshold)
                .OrderBy(hr => hr.Timestamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<HeartRateReading>> GetReadingsByPatientAndDateRangeAsync(string patientId, DateTime startDate, DateTime endDate)
        {
            return await _context.HeartRateReadings
                .Where(hr => hr.PatientId == patientId && hr.Timestamp >= startDate && hr.Timestamp <= endDate)
                .OrderBy(hr => hr.Timestamp)
                .ToListAsync();
        }
    }
}
