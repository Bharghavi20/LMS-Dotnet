using LMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentsController : ControllerBase
{
    private readonly LMSDbContext _context;

    public EnrollmentsController(LMSDbContext context)
    {
        _context = context;
    }
    // POST: api/enrollments
    [HttpPost]
    public async Task<IActionResult> EnrollUser(Enrollment enrollment)
    {
        // 1️⃣ Check user exists
        var userExists = await _context.Users
            .AnyAsync(u => u.UserId == enrollment.UserId);

        if (!userExists)
            return BadRequest("User does not exist");

        // 2️⃣ Check course exists
        var courseExists = await _context.Courses
            .AnyAsync(c => c.CourseId == enrollment.CourseId);

        if (!courseExists)
            return BadRequest("Course does not exist");

        // 3️⃣ Prevent duplicate enrollment
        var alreadyEnrolled = await _context.Enrollments.AnyAsync(e =>
            e.UserId == enrollment.UserId &&
            e.CourseId == enrollment.CourseId);

        if (alreadyEnrolled)
            return BadRequest("User already enrolled in this course");

        // 4️⃣ Save enrollment
        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();

        return Ok(enrollment);
    }
    // GET: api/enrollments/user/21
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetCoursesByUser(int userId)
    {
        var courses = await _context.Enrollments
            .Where(e => e.UserId == userId)
            .Join(
                _context.Courses,
                e => e.CourseId,
                c => c.CourseId,
                (e, c) => new
                {
                    c.CourseId,
                    c.Title,
                    c.Description
                }
            )
            .ToListAsync();

        return Ok(courses);
    }
    // GET: api/enrollments/course/23
    [HttpGet("course/{courseId}")]
    public async Task<IActionResult> GetUsersByCourse(int courseId)
    {
        var users = await _context.Enrollments
            .Where(e => e.CourseId == courseId)
            .Join(
                _context.Users,
                e => e.UserId,
                u => u.UserId,
                (e, u) => new
                {
                    u.UserId,
                    u.FullName,
                    u.Email
                }
            )
            .ToListAsync();

        return Ok(users);
    }



}
