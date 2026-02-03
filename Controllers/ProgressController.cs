using LMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProgressController : ControllerBase
{
    private readonly LMSDbContext _context;

    public ProgressController(LMSDbContext context)
    {
        _context = context;
    }
    // POST: api/progress
    [HttpPost]
    public async Task<IActionResult> MarkLessonCompleted(Progress progress)
    {
        // 1️⃣ Validate user
        if (!await _context.Users.AnyAsync(u => u.UserId == progress.UserId))
            return BadRequest("User does not exist");

        // 2️⃣ Validate lesson
        if (!await _context.Lessons.AnyAsync(l => l.LessonId == progress.LessonId))
            return BadRequest("Lesson does not exist");

        // 3️⃣ Oracle-safe fetch
        var progresses = await _context.Progresses
            .Where(p => p.UserId == progress.UserId && p.LessonId == progress.LessonId)
            .ToListAsync();

        var existing = progresses.FirstOrDefault();

        if (existing != null)
        {
            existing.IsCompleted = "Y";
            existing.CompletedAt = DateTime.Now;
        }
        else
        {
            progress.IsCompleted = "Y";
            progress.CompletedAt = DateTime.Now;
            _context.Progresses.Add(progress);
        }

        await _context.SaveChangesAsync();
        return Ok("Lesson marked as completed");
    }
    // GET: api/progress/user/21/course/23
    [HttpGet("user/{userId}/course/{courseId}")]
    public async Task<IActionResult> GetCourseProgress(int userId, int courseId)
    {
        // Total lessons in course
        var totalLessons = await _context.Lessons
            .Where(l => l.CourseId == courseId)
            .CountAsync();

        if (totalLessons == 0)
            return Ok(new { progress = "0%", completed = 0, total = 0 });

        // Completed lessons
        var completedLessons = await _context.Progresses
            .Where(p =>
                p.UserId == userId &&
                p.IsCompleted == "Y" &&
                _context.Lessons.Any(l => l.LessonId == p.LessonId && l.CourseId == courseId)
            )
            .CountAsync();

        var percentage = (completedLessons * 100) / totalLessons;

        return Ok(new
        {
            completedLessons,
            totalLessons,
            percentage = percentage + "%"
        });
    }


}
