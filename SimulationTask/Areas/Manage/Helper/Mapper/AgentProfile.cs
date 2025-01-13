using AutoMapper;
using SimulationTask.Areas.Manage.Helper.DTOs.Agent;
using SimulationTask.Models;

namespace SimulationTask.Areas.Manage.Helper.Mapper
{
    public class AgentProfile:Profile
    {
        public AgentProfile()
        {
            CreateMap<UpdateAgentDto, User>().ReverseMap();
            CreateMap<CreateAgentDto, User>().ReverseMap();
        }
    }
}
