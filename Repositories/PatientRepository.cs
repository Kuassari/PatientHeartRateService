using Microsoft.EntityFrameworkCore;
using PatientHeartRateService.Data;
using PatientHeartRateService.Models;

namespace PatientHeartRateService.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HeartRateContext _context;

        public PatientRepository(HeartRateContext context)
        {
            _context = context;
        }

        public async Task<Patient?> GetByIdAsync(string id)
        {
            return await _context.Patients
                .Include(p => p.HeartRateReadings)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _context.Patients
                .Include(p => p.HeartRateReadings)
                .ToListAsync();
        }

        public async Task IncrementRequestCountAsync(string patientId)
        {
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient != null)
            {
                patient.RequestCount++;
                await _context.SaveChangesAsync();
            }
        }
    }
}
