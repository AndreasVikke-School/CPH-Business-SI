using System;
using AutoMapper;
using MiniProject1.ClassLib.Protos;

namespace MiniProject1.ClassLib.Modules
{
    public class ProtoMapper<T, E> 
    {
        public static E Map(T source) {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T, E>();
            });
            IMapper iMapper = config.CreateMapper();
            return iMapper.Map<T, E>(source);
        }

        public static E MapCourse(T source)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T, E>().ReverseMap();
                cfg.CreateMap<Student, StudentObj>().ReverseMap();
                cfg.CreateMap<Course, CourseObj>().ReverseMap();
                cfg.CreateMap<Room, RoomObj>().ReverseMap();
                cfg.CreateMap<Book, BookObj>().ReverseMap();
            });
            IMapper iMapper = config.CreateMapper();
            return iMapper.Map<T, E>(source);
        }
    }
}