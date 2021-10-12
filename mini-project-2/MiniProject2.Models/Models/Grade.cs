using MiniProject2.ClassLib.Models.Types;

namespace MiniProject2.ClassLib.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public Student? Student { get; set; }
        public Exam? Exam { get; set; }
        public GradeEnum ActualGrade { get; set; } //evt lav en enum her

    }
}