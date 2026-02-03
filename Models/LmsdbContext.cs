using Microsoft.EntityFrameworkCore;

namespace LMS.Models;

public class LMSDbContext : DbContext
{
    public LMSDbContext(DbContextOptions<LMSDbContext> options)
        : base(options)
    {
    }

    // DbSets (Tables)
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<Progress> Progresses => Set<Progress>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // USERS
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("USERS");
            entity.HasKey(e => e.UserId);

            entity.Property(e => e.UserId).HasColumnName("USER_ID");
            entity.Property(e => e.FullName).HasColumnName("FULL_NAME");
            entity.Property(e => e.Email).HasColumnName("EMAIL");
            entity.Property(e => e.PasswordHash).HasColumnName("PASSWORD_HASH");
            entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
        });

        // ROLES
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("ROLES");
            entity.HasKey(e => e.RoleId);

            entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");
            entity.Property(e => e.RoleName).HasColumnName("ROLE_NAME");
        });

        // USER_ROLES (Composite Key)
        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("USER_ROLES");
            entity.HasKey(e => new { e.UserId, e.RoleId });

            entity.Property(e => e.UserId).HasColumnName("USER_ID");
            entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");
        });

        // COURSES
        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("COURSES");
            entity.HasKey(e => e.CourseId);

            entity.Property(e => e.CourseId).HasColumnName("COURSE_ID");
            entity.Property(e => e.Title).HasColumnName("TITLE");
            entity.Property(e => e.Description).HasColumnName("DESCRIPTION");
            entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
        });

        // ENROLLMENTS
        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.ToTable("ENROLLMENTS");
            entity.HasKey(e => e.EnrollmentId);

            entity.Property(e => e.EnrollmentId).HasColumnName("ENROLLMENT_ID");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");
            entity.Property(e => e.CourseId).HasColumnName("COURSE_ID");
            entity.Property(e => e.EnrolledAt).HasColumnName("ENROLLED_AT");
        });

        // LESSONS
        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.ToTable("LESSONS");
            entity.HasKey(e => e.LessonId);

            entity.Property(e => e.LessonId).HasColumnName("LESSON_ID");
            entity.Property(e => e.CourseId).HasColumnName("COURSE_ID");
            entity.Property(e => e.Title).HasColumnName("TITLE");
            entity.Property(e => e.Content).HasColumnName("CONTENT");
        });

        // PROGRESS
        modelBuilder.Entity<Progress>(entity =>
        {
            entity.ToTable("PROGRESS");
            entity.HasKey(e => e.ProgressId);

            entity.Property(e => e.ProgressId).HasColumnName("PROGRESS_ID");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");
            entity.Property(e => e.LessonId).HasColumnName("LESSON_ID");
            entity.Property(e => e.IsCompleted).HasColumnName("IS_COMPLETED");
            entity.Property(e => e.CompletedAt).HasColumnName("COMPLETED_AT");
        });
    }
}
