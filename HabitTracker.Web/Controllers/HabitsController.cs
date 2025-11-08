using HabitTracker.Web.Interfaces;
using HabitTracker.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HabitTracker.Web.Controllers;

[Authorize]
public class HabitsController : Controller
{
    private readonly IHabitRepository _habitRepository;
    private readonly IHabitService _habitService;
    private readonly UserManager<ApplicationUser> _userManager;

    public HabitsController(
        IHabitRepository habitRepository,
        IHabitService habitService,
        UserManager<ApplicationUser> userManager)
    {
        _habitRepository = habitRepository;
        _habitService = habitService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null) return Challenge();

        var habits = await _habitRepository.GetHabitsForUserAsync(userId);

        // Update streaks for all habits
        foreach (var habit in habits)
        {
            await _habitService.UpdateStreakAsync(habit);
        }

        return View(habits);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Habit habit)
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null) return Challenge();

        habit.ApplicationUserId = userId;
        habit.CreatedAt = DateTime.UtcNow;
        habit.CurrentStreak = 0;

        // Remove validation errors for navigation properties
        ModelState.Remove("ApplicationUser");
        ModelState.Remove("HabitEntries");
        ModelState.Remove("ApplicationUserId");

        if (ModelState.IsValid)
        {
            try
            {
                await _habitRepository.CreateAsync(habit);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating habit: {ex.Message}");
            }
        }

        // Log validation errors for debugging
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine($"Validation Error: {error.ErrorMessage}");
        }

        return View(habit);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var habit = await _habitRepository.GetByIdAsync(id);
        if (habit == null) return NotFound();

        var userId = _userManager.GetUserId(User);
        if (habit.ApplicationUserId != userId) return Forbid();

        return View(habit);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Habit habit)
    {
        if (id != habit.Id) return NotFound();

        // Verify the habit exists and belongs to the current user
        var existingHabit = await _habitRepository.GetByIdAsync(id);
        if (existingHabit == null) return NotFound();

        var userId = _userManager.GetUserId(User);
        if (existingHabit.ApplicationUserId != userId) return Forbid();

        // Remove validation errors for navigation properties
        ModelState.Remove("ApplicationUser");
        ModelState.Remove("HabitEntries");

        if (ModelState.IsValid)
        {
            // Ensure the userId cannot be changed
            habit.ApplicationUserId = userId!;
            habit.CreatedAt = existingHabit.CreatedAt;
            
            await _habitRepository.UpdateAsync(habit);
            return RedirectToAction(nameof(Index));
        }

        return View(habit);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var habit = await _habitRepository.GetByIdAsync(id);
        if (habit == null) return NotFound();

        var userId = _userManager.GetUserId(User);
        if (habit.ApplicationUserId != userId) return Forbid();

        return View(habit);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var habit = await _habitRepository.GetByIdAsync(id);
        if (habit == null) return NotFound();

        var userId = _userManager.GetUserId(User);
        if (habit.ApplicationUserId != userId) return Forbid();

        await _habitRepository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Track(int habitId)
    {
        var habit = await _habitRepository.GetByIdAsync(habitId);
        if (habit == null) return NotFound();

        var userId = _userManager.GetUserId(User);
        if (habit.ApplicationUserId != userId) return Forbid();

        var today = DateTime.UtcNow.Date;
        var existingEntry = await _habitRepository.GetHabitEntryAsync(habitId, today);

        if (existingEntry == null)
        {
            var entry = new HabitEntry
            {
                HabitId = habitId,
                Date = today,
                Done = true
            };
            await _habitRepository.AddHabitEntryAsync(entry);
        }

        await _habitService.UpdateStreakAsync(habit);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Progress(int id)
    {
        var habit = await _habitRepository.GetByIdAsync(id);
        if (habit == null) return NotFound();

        var userId = _userManager.GetUserId(User);
        if (habit.ApplicationUserId != userId) return Forbid();

        return View(habit);
    }

    [HttpGet]
    public async Task<IActionResult> ProgressData(int habitId)
    {
        var habit = await _habitRepository.GetByIdAsync(habitId);
        if (habit == null) return NotFound();

        var userId = _userManager.GetUserId(User);
        if (habit.ApplicationUserId != userId) return Forbid();

        var data = await _habitService.GetProgressDataAsync(habitId);
        return Json(data);
    }
}
