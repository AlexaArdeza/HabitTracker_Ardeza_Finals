# Git Commit Guide for Habit Tracker

This guide helps you commit your code to GitHub following the requirement of **≤ 5 files per commit**.

## Initial Setup

```bash
# Initialize Git repository
git init

# Add the remote repository (replace with your GitHub repo URL)
git remote add origin https://github.com/YOUR_USERNAME/HabitTracker.git
```

## Recommended Commit Strategy

### Commit 1: Project Configuration
```bash
git add .gitignore README.md HabitTracker.Web/HabitTracker.Web.csproj HabitTracker.Web/appsettings.json HabitTracker.Web/Program.cs
git commit -m "Initial project setup with configuration files"
git push -u origin main
```

### Commit 2: Data Models
```bash
git add HabitTracker.Web/Models/ApplicationUser.cs HabitTracker.Web/Models/Habit.cs HabitTracker.Web/Models/HabitEntry.cs HabitTracker.Web/Data/ApplicationDbContext.cs
git commit -m "Add entity models and DbContext"
git push
```

### Commit 3: ViewModels and Security
```bash
git add HabitTracker.Web/Models/RegisterViewModel.cs HabitTracker.Web/Models/LoginViewModel.cs HabitTracker.Web/Security/CustomPasswordValidator.cs
git commit -m "Add ViewModels and custom password validator"
git push
```

### Commit 4: Repository Layer
```bash
git add HabitTracker.Web/Interfaces/IHabitRepository.cs HabitTracker.Web/Repositories/HabitRepository.cs
git commit -m "Implement repository layer"
git push
```

### Commit 5: Service Layer
```bash
git add HabitTracker.Web/Interfaces/IHabitService.cs HabitTracker.Web/Services/HabitService.cs
git commit -m "Implement service layer with business logic"
git push
```

### Commit 6: Controllers
```bash
git add HabitTracker.Web/Controllers/AccountController.cs HabitTracker.Web/Controllers/HabitsController.cs
git commit -m "Add Account and Habits controllers"
git push
```

### Commit 7: Account Views
```bash
git add "HabitTracker.Web/Views/Account/Register.cshtml" "HabitTracker.Web/Views/Account/Login.cshtml"
git commit -m "Add Account views for registration and login"
git push
```

### Commit 8: Habits Views Part 1
```bash
git add "HabitTracker.Web/Views/Habits/Index.cshtml" "HabitTracker.Web/Views/Habits/Create.cshtml" "HabitTracker.Web/Views/Habits/Edit.cshtml"
git commit -m "Add Habits Index, Create, and Edit views"
git push
```

### Commit 9: Habits Views Part 2
```bash
git add "HabitTracker.Web/Views/Habits/Delete.cshtml" "HabitTracker.Web/Views/Habits/Progress.cshtml"
git commit -m "Add Habits Delete and Progress views with Chart.js"
git push
```

### Commit 10: Layout and Home
```bash
git add "HabitTracker.Web/Views/Shared/_Layout.cshtml" "HabitTracker.Web/Views/Home/Index.cshtml"
git commit -m "Update layout and home page"
git push
```

### Commit 11: Migrations
```bash
git add HabitTracker.Web/Migrations/
git commit -m "Add EF Core migrations for database schema"
git push
```

## Alternative: Interactive Staging

You can also stage files interactively to ensure you don't exceed 5 files per commit:

```bash
# View status
git status

# Stage files one by one
git add <file1> <file2> <file3> <file4> <file5>

# Check what's staged
git status

# Commit
git commit -m "Your commit message"

# Push
git push
```

## Verify Your Remote

Before pushing, verify your remote repository:

```bash
git remote -v
```

## Check Commit History

After pushing, you can verify your commits:

```bash
git log --oneline
```

## Important Notes

- Each commit should contain **no more than 5 files**
- Make sure all commits have descriptive messages
- Test the application locally before pushing
- The database files (.mdf, .ldf) are in .gitignore and won't be committed
- The bin/ and obj/ folders are also excluded

## Troubleshooting

If you need to undo a commit (before pushing):
```bash
git reset --soft HEAD~1
```

If you accidentally staged too many files:
```bash
git reset HEAD <file>
```

---

**Remember:** The requirement is ≤ 5 files per commit, so plan your commits accordingly!
