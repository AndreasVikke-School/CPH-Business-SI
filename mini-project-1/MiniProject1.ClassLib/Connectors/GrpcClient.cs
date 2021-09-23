using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
