namespace LMS.DTOs;

public class Register
{
    public string FullName { get; set; }
    public string Username { get; set; }   // ✅ NEW
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}
