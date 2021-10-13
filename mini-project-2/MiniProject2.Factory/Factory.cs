using Google.Protobuf.WellKnownTypes;
using MiniProject2.Factory.Clients;
using MiniProject2.Models.DTO;

namespace MiniProject2.Factory{

  public class Transformer
  {
    public static async Task<StudentDTO> getStudentById(int id)
    {
      return await StudentClient.GetStudentByIdAsync(id);
    }

    public static async Task<List<StudentDTO>> getStudents()
    {
      return await StudentClient.GetStudentsAsync();
    }

    public static async Task<StudentDTO> AddStudent(AddStudentDTO student)
    {
      return await StudentClient.AddStudentsAsync(student);
    }
  }
}