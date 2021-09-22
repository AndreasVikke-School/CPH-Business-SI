using System;
using System.Collections.Generic;

namespace MiniProject1.ClassLib.Modules
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Room room { get; set; }
        public List<Student> students { get; set; }
        public List<Book> books { get; set; }
    }
}