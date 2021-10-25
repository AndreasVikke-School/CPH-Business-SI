using Google.Protobuf.WellKnownTypes;
using MiniProject2.Factory.Clients;
using MiniProject2.Models.DTO;

namespace MiniProject2.Factory{

  public class ExamFactory
  {
    public static async Task<ExamDTO> getExamById(long id)
    {
      ExamDTO exam = await ExamClient.GetExamByIdAsync(id);
      List<StudentDTO> students = new List<StudentDTO>();
      foreach(var a in exam.Students) {
        students.Add(await StudentClient.GetStudentByIdAsync(a.Id));
      }
      exam.Students = students;
      return exam;
    }

    public static async Task<List<ExamDTO>> getExams()
    {
      return await ExamClient.GetExamsAsync();
    }

    public static async Task<ExamDTO> AddExam(AddExamDTO exam)
    {
      ExamDTO ex = await ExamClient.AddExamAsync(exam);
      List<StudentDTO> students = new List<StudentDTO>();
      foreach(var sid in ex.StudentIds) {
        students.Add(await StudentClient.GetStudentByIdAsync(sid));
      }
      ex.Students = students;
      return ex;
    }
  }
}