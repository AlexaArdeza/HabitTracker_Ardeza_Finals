using HabitTracker.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace HabitTracker.Web.Security;

public class CustomPasswordValidator : IPasswordValidator<ApplicationUser>
{
    public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string? password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return Task.FromResult(IdentityResult.Failed(
                new IdentityError { Description = "Password cannot be empty." }));
        }

        var errors = new List<IdentityError>();

        // Check for at least 2 uppercase letters
        int uppercaseCount = password.Count(char.IsUpper);
        if (uppercaseCount < 2)
        {
            errors.Add(new IdentityError
            {
                Code = "PasswordRequiresUppercase",
                Description = "Password must contain at least 2 uppercase letters."
            });
        }

        // Check for at least 3 digits
        int digitCount = password.Count(char.IsDigit);
        if (digitCount < 3)
        {
            errors.Add(new IdentityError
            {
                Code = "PasswordRequiresDigit",
                Description = "Password must contain at least 3 digits."
            });
        }

        // Check for at least 3 symbols (non-alphanumeric characters)
        int symbolCount = password.Count(c => !char.IsLetterOrDigit(c));
        if (symbolCount < 3)
        {
            errors.Add(new IdentityError
            {
                Code = "PasswordRequiresSymbol",
                Description = "Password must contain at least 3 symbols."
            });
        }

        return Task.FromResult(errors.Count == 0
            ? IdentityResult.Success
            : IdentityResult.Failed(errors.ToArray()));
    }
}
