using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using MiniProject1.ClassLib.Modules;
using MiniProject1.Grpc.DatabaseContexts;
using MiniProject1.Grpc.Protos;

namespace MiniProject1.Grpc.Services {

    public class CourseService : CourseProto.CourseProtoBase{
        private readonly ILogger<CourseService> _logger;
        public CourseService(ILogger<CourseService> logger)
        {
            _logger = logger;
        }

        public override Task<CourseReply> GetCourseById(CourseRequest request, ServerCallContext context) {
            using(var dbContext = new SchoolContext()) {
                Course result = dbContext.Courses.Where(c => c.Id == request.Id).FirstOrDefault();

                return Task.FromResult(new CourseReply {
                    Id = result.Id,
                    CourseObj = ProtoMapper<Course, CourseObj>.Map(result)
                });
            }
        }

        public override Task<AllCoursesReply> GetAllCourses(Empty empty, ServerCallContext context) {
            using(var dbContext = new SchoolContext()) {
                List<Course> courses = dbContext.Courses.ToList();

                AllCoursesReply reply = new AllCoursesReply{};
                courses.ForEach(c => reply.Courses.Add(
                    ProtoMapper<Course, CourseObj>.Map(c)
                ));

                return Task.FromResult(reply);
            }
        }

        public override Task<CourseReply> AddCourse(CourseReply input, ServerCallContext context) {
            using (var dbContext = new SchoolContext()) {
                var course = new Course { Name = input.CourseObj.Name};
                dbContext.Courses.Add(course);

                dbContext.SaveChanges();
                
                return Task.FromResult(new CourseReply {
                    Id = course.Id,
                    CourseObj = ProtoMapper<Course, CourseObj>.Map(course)
                });
            }
        }
            
    }
}