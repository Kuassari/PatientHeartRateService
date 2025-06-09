using PatientHeartRateService.DTOs;
using PatientHeartRateService.Repositories;

namespace PatientHeartRateService.Services
{
    public class PatientTrackingService : IPatientTrackingService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientTrackingService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<IEnumerable<PatientRequestTrackingDto>> GetPatientRequestTrackingAsync()
        {
            var patients = await _patientRepository.GetAllAsync();

            return patients.Select(p => new PatientRequestTrackingDto
            {
                PatientId = p.Id,
                PatientName = p.Name,
                RequestCount = p.RequestCount
            });
        }

        public async Task TrackPatientRequestAsync(string patientId)
        {
            await _patientRepository.IncrementRequestCountAsync(patientId);
        }
    }
}
