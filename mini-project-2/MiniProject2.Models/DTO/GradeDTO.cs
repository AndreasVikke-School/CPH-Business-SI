using MiniProject2.Factory.DTO.Types;

namespace MiniProject2.Models.DTO
{
    public class GradeDTO
    {
        public long Id { get; set; }
        public StudentDTO Student { get; set; }
        public ExamDTO Exam { get; set; }
        public int ActualGrade { get; set; }
    }
}