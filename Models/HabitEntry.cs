using System.ComponentModel.DataAnnotations;

namespace HabitTracker.Web.Models;

public class HabitEntry
{
    public int Id { get; set; }
    
    [Required]
    public int HabitId { get; set; }
    
    public Habit Habit { get; set; } = null!;
    
    [Required]
    public DateTime Date { get; set; }
    
    public bool Done { get; set; }
}
