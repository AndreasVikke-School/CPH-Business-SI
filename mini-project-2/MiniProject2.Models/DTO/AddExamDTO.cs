using System;

namespace MiniProject2.Models.DTO
{
    public class AddExamDTO {
        public string Name { get; set;}
        public DateTime Date { get; set; }
        public List<long> StudentIds {get; set;}
    }

    public class AddStudentToExamDTO {
        public long StudentId { get; set;}
    }
    
}