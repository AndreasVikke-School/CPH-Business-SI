using System;

namespace MiniProject2.Factory.DTO
{
    public class ExamDTO {
        public int Id { get; set;}
        public string Name { get; set;}
        public List<StudentDTO> Students { get; set;}
    }
}