using System;
using System.Collections.Generic;

namespace MiniProject1.ClassLib.Modules
{
    public class Room
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public List<Course> Courses { get; set; }
    }
}