# Habit Tracker

A web application for tracking daily habits, building streaks, and visualizing progress.

**Author:** Alexa Mei Ardeza  
**Framework:** ASP.NET Core MVC (.NET 9)  
**Database:** SQL Server LocalDB  

## Features

- ✅ User registration and login with ASP.NET Identity
- ✅ Custom password validation (2 uppercase, 3 digits, 3 symbols)
- ✅ Create, edit, and delete habits
- ✅ Track daily habit completions
- ✅ Automatic streak calculation
- ✅ 30-day progress visualization with Chart.js
- ✅ Clean layered architecture (MVC + Repository + Service)
- ✅ Entity Framework Core Code-First approach

## Architecture

```
HabitTracker.Web/
├── Controllers/        # MVC Controllers (Habits, Account)
├── Data/              # DbContext
├── Interfaces/        # Repository and Service interfaces
├── Models/            # Entity models and ViewModels
├── Repositories/      # Data access layer
├── Security/          # Custom password validator
├── Services/          # Business logic layer
└── Views/             # Razor views
```

## Technologies Used

- ASP.NET Core 9.0 MVC
- Entity Framework Core 9.0
- SQL Server LocalDB
- ASP.NET Core Identity
- Bootstrap 5
- Chart.js
- Dependency Injection

## Database Schema

### ApplicationUser
- Inherits from `IdentityUser`
- DisplayName (string)

### Habit
- Id (int)
- Name (string, required, max 100)
- Description (string, max 500)
- ApplicationUserId (string, FK)
- CreatedAt (DateTime)
- CurrentStreak (int)

### HabitEntry
- Id (int)
- HabitId (int, FK)
- Date (DateTime)
- Done (bool)
- Unique constraint on (HabitId, Date)

## Setup Instructions

1. **Restore packages:**
   ```bash
   dotnet restore
   ```

2. **Update database:**
   ```bash
   dotnet ef database update --project HabitTracker.Web/HabitTracker.Web.csproj
   ```

3. **Run the application:**
   ```bash
   dotnet run --project HabitTracker.Web/HabitTracker.Web.csproj
   ```

4. **Access the app:**
   - Navigate to `https://localhost:5001` or `http://localhost:5000`

## Password Requirements

When registering, passwords must contain:
- At least 2 uppercase letters
- At least 3 digits
- At least 3 symbols

Example valid password: `MyPass123!!!`

## Usage

1. **Register** a new account
2. **Login** with your credentials
3. **Create habits** you want to track (e.g., "Exercise", "Study", "Meditate")
4. **Mark habits as done** each day by clicking "Mark Done Today"
5. **View your streaks** on the habits list page
6. **View progress** with a 30-day chart for each habit

## Project Structure Details

- **Models**: Entity classes representing database tables
- **Data**: ApplicationDbContext for EF Core
- **Interfaces**: IHabitRepository, IHabitService
- **Repositories**: HabitRepository with CRUD operations
- **Services**: HabitService with streak calculation logic
- **Security**: CustomPasswordValidator for password rules
- **Controllers**: HabitsController (authorized), AccountController (public)
- **Views**: Razor pages with Bootstrap styling

## Key Features Implementation

### Streak Calculation
Streaks are calculated by checking consecutive days with completed entries, ending with today or yesterday.

### Progress Chart
Uses Chart.js to display a 30-day bar chart showing completion status (0 or 1) for each day.

### Authentication
ASP.NET Core Identity handles user authentication with cookie-based sessions.

## License

This is a school project created for educational purposes.

---

**Developed by Alexa Mei Ardeza**
