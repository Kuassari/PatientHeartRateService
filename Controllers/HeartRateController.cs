using Microsoft.AspNetCore.Mvc;
using PatientHeartRateService.Services;

namespace PatientHeartRateService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeartRateController : ControllerBase
    {
        private readonly IHeartRateService _heartRateService;
        private readonly IPatientTrackingService _trackingService;

        public HeartRateController(IHeartRateService heartRateService,IPatientTrackingService trackingService)
        {
            _heartRateService = heartRateService;
            _trackingService = trackingService;
        }

        /// <summary>
        /// Get all heart rate readings that exceeded 100 bpm
        /// </summary>
        [HttpGet("GetAllDangerousReadings")]
        public async Task<IActionResult> GetAllDangerousReadings()
        {
            try
            {
                var events = await _heartRateService.GetHighHeartRateEventsAsync();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get heart rate analytics for a specific patient within a specific time frame
        /// </summary>
        [HttpGet("GetPatientStatistics/{patientId}")]
        public async Task<IActionResult> GetPatientStatistics(
            string patientId,
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            if (string.IsNullOrEmpty(patientId))
                return BadRequest("Patient ID is required");

            if (startDate >= endDate)
                return BadRequest("Start date must be before end date");

            try
            {
                var analytics = await _heartRateService.GetHeartRateAnalyticsAsync(patientId, startDate, endDate);

                if (analytics == null)
                    return NotFound($"No data found for patient {patientId} in the specified date range");

                // Track that this patient's data was requested
                await _trackingService.TrackPatientRequestAsync(patientId);

                return Ok(analytics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get heart rate statistics for all patients within a specific time frame
        /// </summary>
        [HttpGet("GetAllPatientsStatistics")]
        public async Task<IActionResult> GetAllPatientsStatistics(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            if (startDate >= endDate)
                return BadRequest("Start date must be before end date");

            try
            {
                var analytics = await _heartRateService.GetAllPatientsAnalyticsAsync(startDate, endDate);

                // Track requests for all patients that had data
                foreach (var analytic in analytics)
                    await _trackingService.TrackPatientRequestAsync(analytic.PatientId);

                return Ok(analytics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get how many times each patient's data has been requested
        /// </summary>
        [HttpGet("GetPatientRequestCounts")]
        public async Task<IActionResult> GetPatientRequestCounts()
        {
            try
            {
                var tracking = await _trackingService.GetPatientRequestTrackingAsync();
                return Ok(tracking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get information about all patients
        /// </summary>
        [HttpGet("GetAllPatients")]
        public async Task<IActionResult> GetAllPatients()
        {
            try
            {
                var patients = await _trackingService.GetPatientRequestTrackingAsync();

                // Return basic patient info without request counts for this endpoint
                var patientInfo = patients.Select(p => new
                {
                    PatientId = p.PatientId,
                    PatientName = p.PatientName
                });

                return Ok(patientInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
