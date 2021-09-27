using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiniProject1.ClassLib.Modules;
using MiniProject1.Grpc.DatabaseContexts;
using MiniProject1.Grpc.Protos;

namespace MiniProject1.Grpc.Services {

    public class CourseService : CourseProto.CourseProtoBase{
        private readonly ILogger<CourseService> _logger;
        private IMapper iMapper;
        public CourseService(ILogger<CourseService> logger)
        {
            _logger = logger;
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Course, CourseObj>().ReverseMap();
                cfg.CreateMap<Student, StudentObj>().ReverseMap();
                cfg.CreateMap<Course, CourseObj>().ReverseMap();
                cfg.CreateMap<Room, RoomObj>().ReverseMap();
                cfg.CreateMap<Book, BookObj>().ReverseMap();
            });
            this.iMapper = config.CreateMapper();
        }

        public override Task<CourseReply> GetCourseById(CourseRequest request, ServerCallContext context) {
            using(var dbContext = new SchoolContext()) {
                Course result = dbContext.Courses
                                        .Where(c => c.Id == request.Id)
                                        .Include(x => x.students)
                                        .Include(x => x.books)
                                        .Include(x => x.room)
                                        .FirstOrDefault();

                return Task.FromResult(new CourseReply {
                    Id = result.Id,
                    CourseObj = iMapper.Map<Course, CourseObj>(result)
                });
            }
        }

        public override Task<AllCoursesReply> GetAllCourses(Empty empty, ServerCallContext context) {
            using(var dbContext = new SchoolContext()) {
                List<Course> courses = dbContext.Courses
                                                .Include(x => x.students)
                                                .Include(x => x.books)
                                                .Include(x => x.room)
                                                .ToList();

                AllCoursesReply reply = new AllCoursesReply{};
                courses.ForEach(c => reply.Courses.Add(
                    iMapper.Map<Course, CourseObj>(c)
                ));

                return Task.FromResult(reply);
            }
        }

        public override Task<CourseReply> AddCourse(CourseReply input, ServerCallContext context) {          
            using (var dbContext = new SchoolContext()) {
                var course = iMapper.Map<CourseObj, Course>(input.CourseObj);
                for(int i = 0; i < course.students.Count; i++) {
                    Student s = dbContext.Students.Where(x => x.Id == course.students[i].Id).FirstOrDefault();
                    if(s != null)
                        course.students[i] = s;
                }
                for (int i = 0; i < course.books.Count; i++)
                {
                    Book b = dbContext.Books.Where(x => x.ISBN == course.books[i].ISBN).FirstOrDefault();
                    if (b != null)
                        course.books[i] = b;
                }
                Room r = dbContext.Rooms.Where(x => x.Id == course.room.Id).FirstOrDefault();
                if (r != null)
                    course.room = r;

                dbContext.Courses.Add(course);
                dbContext.SaveChanges();
                
                return Task.FromResult(new CourseReply {
                    Id = course.Id,
                    CourseObj = iMapper.Map<Course, CourseObj>(course)
                });
            }
        }
            
    }
}