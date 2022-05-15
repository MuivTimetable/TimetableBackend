using AutoMapper;
using TimetableAPI.Dtos;

namespace TimetableAPI.Profiles
{
    public class MapperConfigProfile : Profile
    {
        public MapperConfigProfile()
        {
            CreateMap<Group, GroupReadDto>();



           // CreateMap<>
        }
    }
}
