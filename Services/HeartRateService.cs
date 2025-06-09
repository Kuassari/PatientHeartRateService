using PatientHeartRateService.DTOs;
using PatientHeartRateService.Repositories;

namespace PatientHeartRateService.Services
{
    public class HeartRateService : IHeartRateService
    {
        private readonly IHeartRateReadingRepository _heartRateRepository;
        private readonly IPatientRepository _patientRepository;

        public HeartRateService(IHeartRateReadingRepository heartRateRepository,IPatientRepository patientRepository)
        {
            _heartRateRepository = heartRateRepository;
            _patientRepository = patientRepository;
        }

        public async Task<IEnumerable<HighHeartRateEventDto>> GetHighHeartRateEventsAsync()
        {
            var events = await _heartRateRepository.GetHighHeartRateEventsAsync();

            return events.Select(e => new HighHeartRateEventDto
            {
                PatientId = e.PatientId,
                PatientName = e.Patient.Name,
                Timestamp = e.Timestamp,
                HeartRate = e.HeartRate
            });
        }

        public async Task<HeartRateAnalyticsDto?> GetHeartRateAnalyticsAsync(string patientId, DateTime startDate, DateTime endDate)
        {
            var patient = await _patientRepository.GetByIdAsync(patientId);
            if (patient == null) return null;

            var readings = await _heartRateRepository.GetReadingsByPatientAndDateRangeAsync(
                patientId, startDate, endDate);

            if (!readings.Any()) return null;

            var heartRates = readings.Select(r => r.HeartRate).ToList();

            return new HeartRateAnalyticsDto
            {
                PatientId = patientId,
                PatientName = patient.Name,
                AverageHeartRate = Math.Round(heartRates.Average(), 2),
                MaxHeartRate = heartRates.Max(),
                MinHeartRate = heartRates.Min(),
                ReadingsCount = heartRates.Count,
                StartDate = startDate,
                EndDate = endDate
            };
        }

        public async Task<IEnumerable<HeartRateAnalyticsDto>> GetAllPatientsAnalyticsAsync(DateTime startDate, DateTime endDate)
        {
            var patients = await _patientRepository.GetAllAsync();
            var analytics = new List<HeartRateAnalyticsDto>();

            foreach (var patient in patients)
            {
                var patientAnalytics = await GetHeartRateAnalyticsAsync(patient.Id, startDate, endDate);
                if (patientAnalytics != null)
                    analytics.Add(patientAnalytics);

            }

            return analytics;
        }
    }
}
