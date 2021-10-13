using Google.Protobuf.WellKnownTypes;
using MiniProject2.Factory.Clients;
using MiniProject2.Models.DTO;

namespace MiniProject2.Factory{

  public class ExamFactory
  {
    public static async Task<ExamDTO> getExamById(long id)
    {
      return await ExamClient.GetExamByIdAsync(id);
    }

    public static async Task<List<ExamDTO>> getExams()
    {
      return await ExamClient.GetExamsAsync();
    }

    public static async Task<ExamDTO> AddExam(AddExamDTO exam)
    {
      return await ExamClient.AddExamAsync(exam);
    }
  }
}