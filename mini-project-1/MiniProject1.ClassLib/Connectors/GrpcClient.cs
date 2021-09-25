using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Net.Client;
using MiniProject1.ClassLib.Modules;
using MiniProject1.ClassLib.Protos;

namespace MiniProject1.ClassLib
{
    public class GrpcClient
    {
        public static async Task<Student> Get(int id)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://grpc:80");
            var client =  new StudentProto.StudentProtoClient(channel);
            var reply = await client.GetStudentByIdAsync(
                              new StudentRequest { Id = id });
            return ProtoMapper<StudentObj, Student>.Map(reply.StudentObj);
        }

        public static async Task<List<Student>> GetAll()
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://grpc:80");
            var client = new StudentProto.StudentProtoClient(channel);
            var reply = await client.GetAllStudentsAsync(new Google.Protobuf.WellKnownTypes.Empty());

            List<Student> students = new List<Student>();
            foreach(StudentObj s in reply.Students)
                students.Add(ProtoMapper<StudentObj, Student>.Map(s));
            return students;
        }

        public static async Task<Course> GetCourse(int id)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://grpc:80");
            var client =  new CourseProto.CourseProtoClient(channel);
            var reply = await client.GetCourseByIdAsync(
                              new CourseRequest { Id = id });
            return ProtoMapper<CourseObj, Course>.Map(reply.CourseObj);
        }

        public static async Task<Course> AddCourse(Course course)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://grpc:80");
            var client =  new CourseProto.CourseProtoClient(channel);
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Course, CourseObj>();
                cfg.CreateMap<Room, RoomObj>();
                cfg.CreateMap<List<Student>, List<StudentObj>>();
                cfg.CreateMap<List<Book>, List<BookObj>>();
            });
            IMapper iMapper = config.CreateMapper();
            var reply = await client.AddCourseAsync(new CourseReply {CourseObj = iMapper.Map<Course, CourseObj>(course)});
            return ProtoMapper<CourseObj, Course>.Map(reply.CourseObj);
        }

        public static async Task<Room> AddRoom(Room room)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://grpc:80");
            var client =  new RoomProto.RoomProtoClient(channel);
            var reply = await client.AddRoomAsync(new RoomReply {RoomObj = ProtoMapper<Room, RoomObj>.Map(room)});
            return ProtoMapper<RoomObj, Room>.Map(reply.RoomObj);
        }
        
        
    }
}
