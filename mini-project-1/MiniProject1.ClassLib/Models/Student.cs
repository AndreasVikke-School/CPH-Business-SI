using System;
using System.Collections.Generic;

namespace MiniProject1.ClassLib.Modules
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Course> courses { get; set; }
    }
}