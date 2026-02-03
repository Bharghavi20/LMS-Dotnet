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
    // POST: api/courses
    [HttpPost]
    public async Task<IActionResult> CreateCourse(Course course)
    {
        // Check if creator exists (Oracle-safe)
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


}
