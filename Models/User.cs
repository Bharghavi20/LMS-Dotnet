using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Models;

public class User
{
    public int UserId { get; set; }

    public string FullName { get; set; }

    [Column("USERNAME")]          // 🔥 REQUIRED FOR ORACLE
    public string Username { get; set; }

    public string Email { get; set; }
   
    public string PasswordHash { get; set; }

    public DateTime CreatedAt { get; set; }
}
