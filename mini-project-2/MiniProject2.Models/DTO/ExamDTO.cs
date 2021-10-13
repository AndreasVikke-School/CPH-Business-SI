namespace MiniProject2.Models.DTO
{
    public class ExamDTO {
        public int Id { get; set;}
        public string Name { get; set;}
        // public DateTime Date { get; set; }
        public List<StudentDTO> Students { get; set;}
        public List<TeacherDTO> Teachers { get; set;}
    }
}