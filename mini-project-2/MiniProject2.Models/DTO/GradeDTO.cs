using MiniProject2.Factory.DTO.Types;

namespace MiniProject2.Models.DTO
{
    public class Grade
    {
        public int Id { get; set; }
        public StudentDTO StudentDTO { get; set; }
        public ExamDTO ExamDTO { get; set; }
        public GradeEnum ActualGrade { get; set; }
    }
}