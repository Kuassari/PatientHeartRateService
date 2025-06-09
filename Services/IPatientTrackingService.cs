using PatientHeartRateService.DTOs;

namespace PatientHeartRateService.Services
{
    public interface IPatientTrackingService
    {
        Task<IEnumerable<PatientRequestTrackingDto>> GetPatientRequestTrackingAsync();
        Task TrackPatientRequestAsync(string patientId);
    }
}
