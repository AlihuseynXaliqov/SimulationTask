using AutoMapper;
using SimulationTask.Areas.Manage.Helper.DTOs.Position;
using SimulationTask.Models;

namespace SimulationTask.Areas.Manage.Helper.Mapper
{
    public class PositionProfile:Profile
    {
        public PositionProfile()
        {
            CreateMap<CreatePositionDto,Position>().ReverseMap();
            CreateMap<UpdatePositionDto, Position>().ReverseMap();
        }
    }
}
