using LMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly LMSDbContext _context;

    public UsersController(LMSDbContext context)
    {
        _context = context;
    }

    // GET: api/users
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(users);
    }
    // POST: api/users
    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        user.CreatedAt = DateTime.Now;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(user);
    }
    // PUT: api/users/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, User updatedUser)
    {
        var user = await _context.Users
            .Where(u => u.UserId == id)
            .ToListAsync();

        var existingUser = user.FirstOrDefault();

        if (existingUser == null)
            return NotFound();

        existingUser.FullName = updatedUser.FullName;
        existingUser.Email = updatedUser.Email;
        existingUser.PasswordHash = updatedUser.PasswordHash;

        await _context.SaveChangesAsync();

        return Ok(existingUser);
    }
    // DELETE: api/users/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        // 1. Courses created by user
        var courses = await _context.Courses
            .Where(c => c.CreatedBy == id)
            .ToListAsync();

        if (courses.Any())
        {
            var courseIds = courses.Select(c => c.CourseId).ToList();

            // 2. Lessons under those courses
            var lessons = await _context.Lessons
                .Where(l => courseIds.Contains(l.CourseId))
                .ToListAsync();

            if (lessons.Any())
            {
                var lessonIds = lessons.Select(l => l.LessonId).ToList();

                // 3. Progress for those lessons
                var lessonProgress = await _context.Progresses
                    .Where(p => lessonIds.Contains(p.LessonId))
                    .ToListAsync();

                if (lessonProgress.Any())
                {
                    _context.Progresses.RemoveRange(lessonProgress);
                    await _context.SaveChangesAsync();
                }

                // 4. Delete lessons
                _context.Lessons.RemoveRange(lessons);
                await _context.SaveChangesAsync();
            }

            // 5. Delete courses
            _context.Courses.RemoveRange(courses);
            await _context.SaveChangesAsync();
        }

        // 6. Enrollments
        var enrollments = await _context.Enrollments
            .Where(e => e.UserId == id)
            .ToListAsync();

        if (enrollments.Any())
        {
            _context.Enrollments.RemoveRange(enrollments);
            await _context.SaveChangesAsync();
        }

        // 7. User roles
        var userRoles = await _context.UserRoles
            .Where(ur => ur.UserId == id)
            .ToListAsync();

        if (userRoles.Any())
        {
            _context.UserRoles.RemoveRange(userRoles);
            await _context.SaveChangesAsync();
        }

        // 8. User
        var users = await _context.Users
            .Where(u => u.UserId == id)
            .ToListAsync();

        var user = users.FirstOrDefault();

        if (user == null)
            return NotFound();

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return Ok("User and all related data deleted successfully");
    }

}
