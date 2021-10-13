using MiniProject2.Factory.Clients;

namespace MiniProject2.Factory
{
  public class BookFactory
  {
    public static async Task<bool> ISBN13Validator(string isbn)
    {
      return await BookClient.ISBN13Validator(isbn);
    }

    public static async Task<bool> ISBN10Validator(string isbn)
    {
      return await BookClient.ISBN10Validator(isbn);
    }
  }
}