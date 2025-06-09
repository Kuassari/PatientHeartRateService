using PatientHeartRateService.Models;

namespace PatientHeartRateService.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient?> GetByIdAsync(string id);
        Task<IEnumerable<Patient>> GetAllAsync();
        Task IncrementRequestCountAsync(string patientId);
    }
}
