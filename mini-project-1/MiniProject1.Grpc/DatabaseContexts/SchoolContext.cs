using System;
using Microsoft.EntityFrameworkCore;
using MiniProject1.ClassLib.Modules;

namespace MiniProject1.Grpc.DatabaseContexts
{
    public class SchoolContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=mssql;Database=Master;User Id=SA;Password=P@ssword123");
        }
    }
}
