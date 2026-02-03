using LMS.Models;
using LMS.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly LMSDbContext _context;

    public AuthController(LMSDbContext context)
    {
        _context = context;
    }

    // POST: api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Register request)

    {
        if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            return BadRequest("Username already taken");

        var user = new User
        {
            FullName = request.FullName,
            Username = request.Username,
            Email = request.Email,
            PasswordHash = request.Password
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var roleEntity = await _context.Roles
            .Where(r => r.RoleName == request.Role)
            .FirstOrDefaultAsync();

        if (roleEntity == null)
            return BadRequest("Invalid role");

        _context.UserRoles.Add(new UserRole
        {
            UserId = user.UserId,
            RoleId = roleEntity.RoleId
        });

        await _context.SaveChangesAsync();

        return Ok(new
        {
            user.UserId,
            user.Username,
            Role = request.Role
        });
    }

    // POST: api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Login request)
    {
        var user = await _context.Users
            .Where(u => u.Username == request.Username &&
                        u.PasswordHash == request.Password)
            .FirstOrDefaultAsync();

        if (user == null)
            return Unauthorized("Invalid username or password");

        var role = await _context.UserRoles
            .Join(_context.Roles,
                ur => ur.RoleId,
                r => r.RoleId,
                (ur, r) => new { ur.UserId, r.RoleName })
            .Where(x => x.UserId == user.UserId)
            .Select(x => x.RoleName)
            .FirstOrDefaultAsync();

        return Ok(new
        {
            user.UserId,
            user.Username,
            Role = role
        });
    }
}
