using HabitTracker.Web.Data;
using HabitTracker.Web.Interfaces;
using HabitTracker.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Web.Repositories;

public class HabitRepository : IHabitRepository
{
    private readonly ApplicationDbContext _context;

    public HabitRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Habit>> GetHabitsForUserAsync(string userId)
    {
        return await _context.Habits
            .Where(h => h.ApplicationUserId == userId)
            .Include(h => h.HabitEntries)
            .ToListAsync();
    }

    public async Task<Habit?> GetByIdAsync(int id)
    {
        return await _context.Habits
            .Include(h => h.HabitEntries)
            .FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task<Habit> CreateAsync(Habit habit)
    {
        _context.Habits.Add(habit);
        await _context.SaveChangesAsync();
        return habit;
    }

    public async Task<Habit> UpdateAsync(Habit habit)
    {
        var existingHabit = await _context.Habits.FindAsync(habit.Id);
        if (existingHabit == null)
        {
            throw new InvalidOperationException($"Habit with ID {habit.Id} not found.");
        }

        // Only update the properties that should be modified
        existingHabit.Name = habit.Name;
        existingHabit.Description = habit.Description;
        existingHabit.CurrentStreak = habit.CurrentStreak;

        await _context.SaveChangesAsync();
        return existingHabit;
    }

    public async Task DeleteAsync(int id)
    {
        var habit = await _context.Habits
            .Include(h => h.HabitEntries)
            .FirstOrDefaultAsync(h => h.Id == id);
        
        if (habit != null)
        {
            _context.Habits.Remove(habit);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<HabitEntry?> GetHabitEntryAsync(int habitId, DateTime date)
    {
        var dateOnly = date.Date;
        return await _context.HabitEntries
            .FirstOrDefaultAsync(he => he.HabitId == habitId && he.Date.Date == dateOnly);
    }

    public async Task<HabitEntry> AddHabitEntryAsync(HabitEntry entry)
    {
        _context.HabitEntries.Add(entry);
        await _context.SaveChangesAsync();
        return entry;
    }

    public async Task<IEnumerable<HabitEntry>> GetHabitEntriesAsync(int habitId, DateTime startDate, DateTime endDate)
    {
        return await _context.HabitEntries
            .Where(he => he.HabitId == habitId && he.Date >= startDate && he.Date <= endDate)
            .OrderBy(he => he.Date)
            .ToListAsync();
    }
}
