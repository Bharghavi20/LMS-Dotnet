namespace LMS.Models
{
    public class Lesson
    {
        public int LessonId { get; set; }

        public int CourseId { get; set; }

        public string Title { get; set; } = null!;

        public string? Content { get; set; }
    }
}
