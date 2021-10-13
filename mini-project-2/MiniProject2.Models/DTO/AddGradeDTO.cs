using MiniProject2.Factory.DTO.Types;
namespace MiniProject2.Models.DTO
{
    public class AddGradeDTO
    {
        public long StudentId { get; set; }
        public long ExamId { get; set; }
        public GradeEnum ActualGrade { get; set; }
    }
}