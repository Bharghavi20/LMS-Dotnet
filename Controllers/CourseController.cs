using LMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS.Helpers;


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
    [HttpPost]
    public async Task<IActionResult> CreateCourse(int userId, Course course)
    {
        // 🔐 ADMIN CHECK
        if (!await RoleChecker.IsAdmin(_context, userId))
            return StatusCode(403, "Only Admin can create courses");


        // Check if creator exists
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
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, int userId, Course updatedCourse)
    {
        // 🔐 ADMIN CHECK
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
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id, int userId)
    {
        // 🔐 ADMIN CHECK
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

            _context.Progresses.RemoveRange(progress);
            _context.Lessons.RemoveRange(lessons);
            await _context.SaveChangesAsync();
        }

        var course = await _context.Courses
            .Where(c => c.CourseId == id)
            .FirstOrDefaultAsync();

        if (course == null)
            return NotFound();

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();

        return Ok("Course and related data deleted successfully");
    }


}
