namespace MiniProject2.Models.DTO
{
    public class ExamDTO {
        public long Id { get; set;}
        public string Name { get; set;}
        public DateTime Date { get; set; }
        public List<StudentDTO> Students { get; set;}
        public List<TeacherDTO> Teachers { get; set;}
        public List<long> StudentIds { get; set;}
    }
}