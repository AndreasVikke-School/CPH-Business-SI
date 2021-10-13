using Google.Protobuf.WellKnownTypes;
using MiniProject2.Factory.Clients;
using MiniProject2.Models.DTO;

namespace MiniProject2.Factory{

  public class TeacherFactory
  {
    public static async Task<TeacherDTO> getTeacherById(int id)
    {
      return await TeacherClient.GetTeacherByIdAsync(id);
    }

    public static async Task<List<TeacherDTO>> getTeachers()
    {
      return await TeacherClient.GetTeachersAsync();
    }

    public static async Task<TeacherDTO> AddTeacher(AddTeacherDTO teacher)
    {
      return await TeacherClient.AddTeacherAsync(teacher);
    }
  }
}