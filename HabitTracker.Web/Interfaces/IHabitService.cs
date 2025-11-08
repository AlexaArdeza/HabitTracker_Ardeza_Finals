using HabitTracker.Web.Models;

namespace HabitTracker.Web.Interfaces;

public interface IHabitService
{
    Task<int> CalculateCurrentStreakAsync(int habitId);
    Task<Dictionary<string, int>> GetProgressDataAsync(int habitId, int days = 30);
    Task UpdateStreakAsync(Habit habit);
}
