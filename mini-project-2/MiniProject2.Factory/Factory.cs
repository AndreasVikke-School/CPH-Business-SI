using MiniProject2.Factory.Clients;
using MiniProject2.Factory.DTO;

namespace MiniProject2.Factory{

  public class Transformer
  {
    public static async Task<StudentDTO> getStudentById(int id)
    {
      return await StudentClient.GetStudentByIdAsync(id);
    }
  }
}