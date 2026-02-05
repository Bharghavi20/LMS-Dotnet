using LMS.Helpers;
using LMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly LMSDbContext _context;

    public CoursesController(LMSDbContext context)
    {
        _context = context;
    }

    // GET: api/courses
    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        var courses = await _context.Courses.ToListAsync();
        return Ok(courses);
    }

    // POST: api/courses?userId=21
    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromQuery] int userId, [FromBody] Course course)
    {
        if (!await RoleChecker.IsAdmin(_context, userId))
            return StatusCode(403, "Only Admin can create courses");

        var users = await _context.Users
            .Where(u => u.UserId == course.CreatedBy)
            .ToListAsync();

        if (!users.Any())
            return BadRequest("CreatedBy user does not exist");

        course.CreatedAt = DateTime.Now;

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        return Ok(course);
    }

    // PUT: api/courses/23?userId=21
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, [FromQuery] int userId, [FromBody] Course updatedCourse)
    {
        if (!await RoleChecker.IsAdmin(_context, userId))
            return StatusCode(403, "Only Admin can update courses");

        var courses = await _context.Courses
            .Where(c => c.CourseId == id)
            .ToListAsync();

        var course = courses.FirstOrDefault();

        if (course == null)
            return NotFound();

        course.Title = updatedCourse.Title;
        course.Description = updatedCourse.Description;

        await _context.SaveChangesAsync();

        return Ok(course);
    }

    // DELETE: api/courses/23?userId=21
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id, [FromQuery] int userId)
    {
        if (!await RoleChecker.IsAdmin(_context, userId))
            return StatusCode(403, "Only Admin can delete courses");

        var lessons = await _context.Lessons
            .Where(l => l.CourseId == id)
            .ToListAsync();

        if (lessons.Any())
        {
            var lessonIds = lessons.Select(l => l.LessonId).ToList();

            var progress = await _context.Progresses
                .Where(p => lessonIds.Contains(p.LessonId))
                .ToListAsync();

            if (progress.Any())
                _context.Progresses.RemoveRange(progress);

            _context.Lessons.RemoveRange(lessons);

            await _context.SaveChangesAsync();
        }

        var courses = await _context.Courses
            .Where(c => c.CourseId == id)
            .ToListAsync();

        var course = courses.FirstOrDefault();

        if (course == null)
            return NotFound();

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();

        return Ok("Course and related data deleted successfully");
    }
}
