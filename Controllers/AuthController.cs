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
        var usernameExists = await _context.Users
            .Where(u => u.Username == request.Username)
            .ToListAsync();

        if (usernameExists.Any())
            return BadRequest("Username already taken");

        var emailExists = await _context.Users
            .Where(u => u.Email == request.Email)
            .ToListAsync();

        if (emailExists.Any())
            return BadRequest("Email already registered");

        var roles = await _context.Roles
            .Where(r => r.RoleName == request.Role)
            .ToListAsync();

        var roleEntity = roles.FirstOrDefault();

        if (roleEntity == null)
            return BadRequest("Invalid role");

        using var transaction = await _context.Database.BeginTransactionAsync();

        var user = new User
        {
            FullName = request.FullName,
            Username = request.Username,
            Email = request.Email,
            PasswordHash = request.Password,
            CreatedAt = DateTime.Now
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var userRole = new UserRole
        {
            UserId = user.UserId,
            RoleId = roleEntity.RoleId
        };

        _context.UserRoles.Add(userRole);
        await _context.SaveChangesAsync();

        await transaction.CommitAsync();

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
        var users = await _context.Users
            .Where(u => u.Username == request.Username
                     && u.PasswordHash == request.Password)
            .ToListAsync();

        var user = users.FirstOrDefault();

        if (user == null)
            return Unauthorized("Invalid username or password");

        return Ok(new
        {
            user.UserId,
            user.Username,
            Message = "Login successful"
        });
    }

}
