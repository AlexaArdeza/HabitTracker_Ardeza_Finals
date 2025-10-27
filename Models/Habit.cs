using System.ComponentModel.DataAnnotations;

namespace HabitTracker.Web.Models;

public class Habit
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    [Required]
    public string ApplicationUserId { get; set; } = string.Empty;
    
    public ApplicationUser ApplicationUser { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public int CurrentStreak { get; set; }
    
    public ICollection<HabitEntry> HabitEntries { get; set; } = new List<HabitEntry>();
}
