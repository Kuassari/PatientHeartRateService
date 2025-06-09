using PatientHeartRateService.Models;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace PatientHeartRateService.Data
{
    public static class SeedData
    {
        public static async Task Initialize(HeartRateContext context, IWebHostEnvironment environment)
        {
            if (await context.Patients.AnyAsync())
                return; 

            var jsonFilePath = Path.Combine(environment.ContentRootPath, "Data", "patients.json");

            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"JSON seed file not found at: {jsonFilePath}");
            }

            var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var jsonData = JsonSerializer.Deserialize<JsonDataRoot>(jsonContent, options);

            if (jsonData == null)
            {
                throw new InvalidOperationException("Failed to deserialize JSON data");
            }

            // Convert JSON patients to database entities
            var patients = jsonData.Patients.Select(jp => new Patient
            {
                Id = jp.Id,
                Name = jp.Name,
                Age = jp.Age,
                Gender = jp.Gender,
                RequestCount = 0
            }).ToList();

            await context.Patients.AddRangeAsync(patients);
            await context.SaveChangesAsync();

            // Convert JSON readings to database entities
            var heartRateReadings = jsonData.HeartRateReadings.Select(jhr => new HeartRateReading
            {
                PatientId = jhr.PatientId,
                Timestamp = jhr.Timestamp.ToUniversalTime(),
                HeartRate = jhr.HeartRate
            }).ToList();

            await context.HeartRateReadings.AddRangeAsync(heartRateReadings);
            await context.SaveChangesAsync();
        }
    }
}
