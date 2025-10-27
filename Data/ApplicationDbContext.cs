using HabitTracker.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Web.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Habit> Habits { get; set; }
    public DbSet<HabitEntry> HabitEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Habit>()
            .HasOne(h => h.ApplicationUser)
            .WithMany(u => u.Habits)
            .HasForeignKey(h => h.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HabitEntry>()
            .HasOne(he => he.Habit)
            .WithMany(h => h.HabitEntries)
            .HasForeignKey(he => he.HabitId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HabitEntry>()
            .HasIndex(he => new { he.HabitId, he.Date })
            .IsUnique();
    }
}
