using Microsoft.EntityFrameworkCore;
using MiniProject2.Models.Models;
using MiniProject2.EF.DatabaseContexts;

namespace MiniProject2.EF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new SchoolContext())
            {
                context.Database.Migrate();
                context.Students.Add(new Student
                    {
                        Name = "Andreas Vikke"
                    });
                // context.Students.Add(new Student
                //     {
                //         Name = "Martin Frederiksen"
                //     });
                context.SaveChanges();

                Student test = context.Students.Where(x => x.Id == 1).SingleOrDefault();
                Console.WriteLine(test.Name);
            }
        }
    }
}