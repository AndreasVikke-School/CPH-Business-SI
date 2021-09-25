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

    public class RoomService : RoomProto.RoomProtoBase{
        private readonly ILogger<RoomService> _logger;
        public RoomService(ILogger<RoomService> logger)
        {
            _logger = logger;
        }

        public override Task<RoomReply> GetRoomById(RoomRequest request, ServerCallContext context) {
            using(var dbContext = new SchoolContext()) {
                Room result = dbContext.Rooms.Where(r => r.Id == request.Id).FirstOrDefault();

                return Task.FromResult(new RoomReply {
                    Id = result.Id,
                    RoomObj = ProtoMapper<Room, RoomObj>.Map(result)
                });
            }
        }

        public override Task<AllRoomsReply> GetAllRooms(Empty empty, ServerCallContext context) {
            using(var dbContext = new SchoolContext()) {
                List<Room> rooms = dbContext.Rooms.ToList();

                AllRoomsReply reply = new AllRoomsReply{};
                rooms.ForEach(r => reply.Rooms.Add(
                    ProtoMapper<Room, RoomObj>.Map(r)
                ));

                return Task.FromResult(reply);
            }
        }

        public override Task<RoomReply> AddRoom(RoomReply input, ServerCallContext context) {
            using (var dbContext = new SchoolContext()) {
                var room = new Room { Capacity = input.RoomObj.Capacity};
                dbContext.Rooms.Add(room);

                dbContext.SaveChanges();
                
                return Task.FromResult(new RoomReply {
                    Id = room.Id,
                    RoomObj = ProtoMapper<Room, RoomObj>.Map(room)
                });
            }
        }
            
    }
}