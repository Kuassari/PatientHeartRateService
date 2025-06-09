using Microsoft.EntityFrameworkCore;
using PatientHeartRateService.Data;
using PatientHeartRateService.Repositories;
using PatientHeartRateService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Entity Framework with In-Memory Database
builder.Services.AddDbContext<HeartRateContext>(options =>
    options.UseInMemoryDatabase("HeartRateDB"));

// Register repositories and services for dependency injection
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IHeartRateReadingRepository, HeartRateReadingRepository>();
builder.Services.AddScoped<IHeartRateService, HeartRateService>();
builder.Services.AddScoped<IPatientTrackingService, PatientTrackingService>();

var app = builder.Build();

// Seed the database with JSON data when app starts
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<HeartRateContext>();
    var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
    await SeedData.Initialize(context, environment);
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();