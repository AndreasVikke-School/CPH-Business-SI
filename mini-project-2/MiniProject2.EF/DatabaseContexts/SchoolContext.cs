using System;
using Microsoft.EntityFrameworkCore;
using MiniProject2.Models.Models;

namespace MiniProject2.EF.DatabaseContexts
{
    public class SchoolContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=mssql;Database=Master;User Id=SA;Password=P@ssword123");
        }
    }
}
