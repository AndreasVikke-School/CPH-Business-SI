using System;
using System.Collections.Generic;

namespace MiniProject2.ClassLib.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<Student> Students { get; set; }
        public List<Teacher> Teachers { get; set; }
    }
}