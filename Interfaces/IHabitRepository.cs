using HabitTracker.Web.Models;

namespace HabitTracker.Web.Interfaces;

public interface IHabitRepository
{
    Task<IEnumerable<Habit>> GetHabitsForUserAsync(string userId);
    Task<Habit?> GetByIdAsync(int id);
    Task<Habit> CreateAsync(Habit habit);
    Task<Habit> UpdateAsync(Habit habit);
    Task DeleteAsync(int id);
    Task<HabitEntry?> GetHabitEntryAsync(int habitId, DateTime date);
    Task<HabitEntry> AddHabitEntryAsync(HabitEntry entry);
    Task<IEnumerable<HabitEntry>> GetHabitEntriesAsync(int habitId, DateTime startDate, DateTime endDate);
}
