using HabitTracker.Web.Interfaces;
using HabitTracker.Web.Models;

namespace HabitTracker.Web.Services;

public class HabitService : IHabitService
{
    private readonly IHabitRepository _habitRepository;

    public HabitService(IHabitRepository habitRepository)
    {
        _habitRepository = habitRepository;
    }

    public async Task<int> CalculateCurrentStreakAsync(int habitId)
    {
        var habit = await _habitRepository.GetByIdAsync(habitId);
        if (habit == null) return 0;

        var entries = habit.HabitEntries
            .Where(e => e.Done)
            .OrderByDescending(e => e.Date)
            .ToList();

        if (!entries.Any()) return 0;

        int streak = 0;
        var today = DateTime.UtcNow.Date;
        var currentDate = today;

        // Allow for today or yesterday as starting point
        if (entries.First().Date.Date != today && entries.First().Date.Date != today.AddDays(-1))
        {
            return 0;
        }

        foreach (var entry in entries)
        {
            if (entry.Date.Date == currentDate || entry.Date.Date == currentDate.AddDays(-1))
            {
                streak++;
                currentDate = entry.Date.Date.AddDays(-1);
            }
            else
            {
                break;
            }
        }

        return streak;
    }

    public async Task<Dictionary<string, int>> GetProgressDataAsync(int habitId, int days = 30)
    {
        var endDate = DateTime.UtcNow.Date;
        var startDate = endDate.AddDays(-(days - 1));

        var entries = await _habitRepository.GetHabitEntriesAsync(habitId, startDate, endDate);
        var entryDict = entries.ToDictionary(e => e.Date.Date, e => e.Done ? 1 : 0);

        var progressData = new Dictionary<string, int>();

        for (int i = 0; i < days; i++)
        {
            var date = startDate.AddDays(i);
            var dateKey = date.ToString("yyyy-MM-dd");
            progressData[dateKey] = entryDict.ContainsKey(date) ? entryDict[date] : 0;
        }

        return progressData;
    }

    public async Task UpdateStreakAsync(Habit habit)
    {
        var streak = await CalculateCurrentStreakAsync(habit.Id);
        habit.CurrentStreak = streak;
        await _habitRepository.UpdateAsync(habit);
    }
}
