using Commander.Dtos;
using Commander.Models;
using AutoMapper;

namespace Commander.Profiles
{
    public class CommandsProfile:Profile
    {

        public CommandsProfile()
        {
            //source==>Target
            CreateMap<Command,CommandReadDto>();
            CreateMap<CommandCreateDto,Command>();
            CreateMap<CommandUpdateDto,Command>();
            CreateMap<Command,CommandUpdateDto>();
        }
    }
}