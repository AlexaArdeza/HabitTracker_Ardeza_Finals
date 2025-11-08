using Microsoft.AspNetCore.Identity;

namespace HabitTracker.Web.Models;

public class ApplicationUser : IdentityUser
{
    public string? DisplayName { get; set; }
    
    public ICollection<Habit> Habits { get; set; } = new List<Habit>();
}
