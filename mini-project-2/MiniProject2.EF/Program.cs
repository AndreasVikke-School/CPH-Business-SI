using Microsoft.EntityFrameworkCore;
using MiniProject2.Models.Models;
using MiniProject2.EF.DatabaseContexts;
using MiniProject2.Models.Models.Types;

namespace MiniProject2.EF
{
    public class Program
    {
        static List<Student> students = new List<Student>(){
            new Student() { Name = "Andreas Vikke"},
            new Student() { Name = "Martin Frederiksen"},
            new Student() { Name = "William Huusfeldt"},
            new Student() { Name = "Asger Sørensen"}
        };
        static List<Teacher> teachers = new List<Teacher>(){
            new Teacher() { Name = "Todorka Stoyanova Dimitrova"}
        };
        static List<Exam> exams = new List<Exam>(){
            new Exam() { Name = "Mini Project 2", Date = new DateTime(2021, 10, 25), Teachers = teachers, Students = students},
            new Exam() { Name = "System Integration", Date = new DateTime(2021, 12, 20), Teachers = teachers, Students = students}
        };
        static List<Grade> grades = new List<Grade>(){
            new Grade() { Student = students[0], Exam = exams[0], ActualGrade = 12 },
            new Grade() { Student = students[1], Exam = exams[0], ActualGrade = 12 },
            new Grade() { Student = students[2], Exam = exams[0], ActualGrade = 12 },
            new Grade() { Student = students[3], Exam = exams[0], ActualGrade = 12 },
            new Grade() { Student = students[0], Exam = exams[1], ActualGrade = 12 },
            new Grade() { Student = students[1], Exam = exams[1], ActualGrade = 12 },
            new Grade() { Student = students[2], Exam = exams[1], ActualGrade = 12 },
            new Grade() { Student = students[3], Exam = exams[1], ActualGrade = 12 }
        };


        public static void Main(string[] args)
        {
            using (var context = new SchoolContext())
            {
                context.Database.Migrate();
                foreach (Student s in students)
                    context.Students.Add(s);
                foreach (Teacher t in teachers)
                    context.Teachers.Add(t);
                foreach (Exam e in exams)
                    context.Exams.Add(e);
                foreach (Grade g in grades)
                    context.Grades.Add(g);
                context.SaveChanges();
            }
        }
    }
}