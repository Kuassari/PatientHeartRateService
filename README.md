# 🏥 Heart Rate Monitoring Service

A backend service for managing patients and their heart rate readings, built with clean architecture principles.

> 🎯 **Take-Home Assignment** | Company interview process (2024)  
> **Time Constraint:** 2-3 hours | Built with .NET Core (C#)


## 📋 Assignment Background

This was a take-home coding challenge during a company's interview process. While I didn't advance to the next round, I'm proud of the solution and believe it demonstrates strong backend development capabilities.
The company provided specific requirements, a JSON file with mock data, and a 2-3 hour time constraint. Focus was on clean architecture and code quality over feature count.


## ✨ Features Implemented

All three required endpoints were successfully delivered:

### 1. High Heart Rate Event Detection ✅
**Endpoint:** `GET /api/HeartRate/GetAllDangerousReadings`

Returns all instances where heart rate exceeded 100 bpm across all patients.


### 2. Heart Rate Analytics ✅
**Endpoint:** `GET /api/HeartRate/GetPatientStatistics/{patientId}?startDate={date}&endDate={date}`

Calculates average, maximum, and minimum heart rate for a patient within a time range.

**Bonus:** `GET /api/HeartRate/GetAllPatientsStatistics` - Analytics for all patients.


### 3. Patient Request Tracking ✅
**Endpoint:** `GET /api/HeartRate/GetPatientRequestCounts`

Tracks how many times each patient's data has been accessed through analytics endpoints.


## 🛠️ Tech Stack

- .NET Core / ASP.NET Core Web API (C#)
- In-memory data context with JSON seeding
- Clean Architecture: Controller → Service → Repository → Data layers
- Repository pattern with interface-based design
- DTOs for clean API contracts
- Async/await throughout


## 🏗️ Project Structure

```
PatientHeartRateService/
├── Controllers/         # API endpoints
├── Services/           # Business logic (IHeartRateService, IPatientTrackingService)
├── Repositories/       # Data access layer (IPatientRepository, IHeartRateReadingRepository)
├── DTOs/              # API response models
├── Models/            # Domain entities (Patient, HeartRateReading, JsonDataModels)
├── Data/              # HeartRateContext, SeedData, patients.json
└── Program.cs         # DI configuration
```


## 🚀 Getting Started

```bash
# Clone and restore
git clone https://github.com/yourusername/heart-rate-monitoring-service.git
cd heart-rate-monitoring-service
dotnet restore

# Run
dotnet run
```

API available at `https://localhost:5001/api`


## 🎯 Key Design Decisions

**Clean Architecture**
- Full separation: Controller → Service → Repository → Data
- Interface-based contracts at service and repository levels
- DTOs to separate domain models from API responses

**Data Storage**
- In-memory context with JSON seeding (pragmatic choice for 2-3 hour constraint)
- `SeedData` loads provided JSON on startup
- Repository pattern makes future database migration trivial

**Error Handling**
- Consistent HTTP status codes (200, 400, 404, 500)
- Input validation at controller level
- Descriptive error messages

**Request Tracking**
- Dedicated `PatientTrackingService` for separation of concerns
- Automatically triggered when analytics endpoints are called


## 💭 Trade-offs & Improvements

**What I Prioritized (2-3 hours):**
- Clean architecture with proper layering
- Interface-based design for testability
- Code organization and readability
- All required features fully working

**Trade-offs Made:**
- In-memory storage instead of database setup (fast path to working solution)
- Limited test coverage (prioritized working code)
- Focused on core features over edge cases

**What I'd Add Next:**
- Entity Framework with real database + migrations
- Authentication & authorization (JWT)
- Comprehensive unit tests
- Caching for analytics queries
- Frontend with Reactjs

## 📝 Reflection

**Technical Highlights:**
- Implemented full clean architecture in under 3 hours
- Separate JSON deserialization models from domain entities
- Multiple DTOs for type-safe, well-organized responses
- Repository pattern for data abstraction
- Added bonus endpoints beyond requirements

**Time Breakdown:**
- Setup & Architecture: 30 min
- Core Implementation: 90 min  
- Testing & Polish: 20 min
- **Total: ~2.5 hours**


---

**Note:** This was a take-home assignment for a company interview (2024). While I didn't receive an offer, this solution demonstrates solid backend architecture and the ability to deliver quality work under tight time constraints.
