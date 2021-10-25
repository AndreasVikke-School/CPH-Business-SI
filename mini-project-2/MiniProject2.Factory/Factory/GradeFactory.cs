using MiniProject2.Factory.Clients;
using MiniProject2.Models.DTO;
using System;

namespace MiniProject2.Factory.Factory
{
    public class GradeFactory
    {

        public static async Task<GradeDTO> getGradeById(long id)
        {
            GradeDTO grade = await GradeClient.GetGradeByIdAsync(id);
            grade.Student = await StudentClient.GetStudentByIdAsync(grade.Student.Id);
            grade.Exam = await ExamClient.GetExamByIdAsync(grade.Exam.Id);
            return grade;
        }

        public static async Task<List<GradeDTO>> getAllGrades()
        {
            List<GradeDTO> grades = await GradeClient.GetGradesAsync();
            foreach (var student in grades)
            {
                student.Student = await StudentClient.GetStudentByIdAsync(student.Student.Id);
            }
            foreach (var exam in grades)
            {
                exam.Exam = await ExamClient.GetExamByIdAsync(exam.Exam.Id);
            }
            return grades;
        }
        public static async Task<GradeDTO> AddGradeToStudent(AddGradeDTO grade)
        {
            return await GradeClient.AddGradeToStudentAsync(grade);
        }

        public static async Task<List<GradeDTO>> getPassedStudents(int examId)
        {
            List<GradeDTO> grades = await GradeClient.getPassedStudentsAsync(examId);
            foreach (var student in grades)
            {
                student.Student = await StudentClient.GetStudentByIdAsync(student.Student.Id);
            }
            foreach (var exam in grades)
            {
                exam.Exam = await ExamClient.GetExamByIdAsync(exam.Exam.Id);
            }
            return grades;
        }

        public static async Task<long> getFailedStudents(int examId)
        {
            return await GradeClient.getFailedStudentsAsync(examId);
        }
    }
}