using LMS.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Helpers;

public static class RoleChecker
{
    // Check if user is Admin
    public static async Task<bool> IsAdmin(LMSDbContext context, int userId)
    {
        return await context.UserRoles
            .Join(context.Roles,
                ur => ur.RoleId,
                r => r.RoleId,
                (ur, r) => new { ur.UserId, r.RoleName })
            .AnyAsync(x => x.UserId == userId && x.RoleName == "Admin");
    }

    // Check if user is Student
    public static async Task<bool> IsStudent(LMSDbContext context, int userId)
    {
        return await context.UserRoles
            .Join(context.Roles,
                ur => ur.RoleId,
                r => r.RoleId,
                (ur, r) => new { ur.UserId, r.RoleName })
            .AnyAsync(x => x.UserId == userId && x.RoleName == "Student");
    }
}
