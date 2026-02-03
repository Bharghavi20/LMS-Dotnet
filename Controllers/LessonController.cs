using LMS.Helpers;
using LMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonsController : ControllerBase
{
    private readonly LMSDbContext _context;

    public LessonsController(LMSDbContext context)
    {
        _context = context;
    }
    // GET: api/lessons/course/22
    [HttpGet("course/{courseId}")]
    public async Task<IActionResult> GetLessonsByCourse(int courseId)
    {
        var lessons = await _context.Lessons
            .Where(l => l.CourseId == courseId)
            .ToListAsync();

        return Ok(lessons);
    }
    // POST: api/lessons
    [HttpPost]
    public async Task<IActionResult> CreateLesson(int userId, Lesson lesson)
    {
        if (!await RoleChecker.IsAdmin(_context, userId))
            return StatusCode(403, "Only Admin can create lessons");

        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync();

        return Ok(lesson);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLesson(int id, int userId, Lesson updatedLesson)
    {
        if (!await RoleChecker.IsAdmin(_context, userId))
            return StatusCode(403, "Only Admin can update lessons");

        var lessons = await _context.Lessons
            .Where(l => l.LessonId == id)
            .ToListAsync();

        var lesson = lessons.FirstOrDefault();
        if (lesson == null)
            return NotFound();

        lesson.Title = updatedLesson.Title;
        lesson.Content = updatedLesson.Content;

        await _context.SaveChangesAsync();
        return Ok(lesson);
    }
    // DELETE: api/lessons/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLesson(int id, int userId)
    {
        if (!await RoleChecker.IsAdmin(_context, userId))
            return StatusCode(403, "Only Admin can delete lessons");

        var lessons = await _context.Lessons
            .Where(l => l.LessonId == id)
            .ToListAsync();

        var lesson = lessons.FirstOrDefault();
        if (lesson == null)
            return NotFound();

        var progress = await _context.Progresses
            .Where(p => p.LessonId == id)
            .ToListAsync();

        _context.Progresses.RemoveRange(progress);
        _context.Lessons.Remove(lesson);

        await _context.SaveChangesAsync();
        return Ok("Lesson deleted successfully");
    }



}
