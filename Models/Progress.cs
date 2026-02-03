namespace LMS.Models
{
    public class Progress
    {
        public int ProgressId { get; set; }
        public int UserId { get; set; }

        public int LessonId { get; set; }
        public string IsCompleted { get; set; } = "N";
        public DateTime? CompletedAt { get; set; }
    }
}
