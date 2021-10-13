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
            return await GradeClient.GetGradesAsync();
        }
        public static async Task<GradeDTO> AddGradeToStudent(AddGradeDTO grade)
        {
            // StudentDTO student = StudentClient.GetStudentByIdAsync(grade.studentId);
            // ExamDTO exam = ExamClient.GetExamByIdAsync(grade.examId);
            return await GradeClient.AddGradeToStudentAsync(grade);
        }
    }
}