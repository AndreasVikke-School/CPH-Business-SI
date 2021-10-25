using MiniProject2.Models.Models.Types;

namespace MiniProject2.Models.Models
{
    public class Grade
    {
        public long Id { get; set; }
        public Student Student { get; set; }
        public Exam Exam { get; set; }
        // public GradeEnum ActualGrade { get; set; }
        public int ActualGrade { get; set; }
    }
}