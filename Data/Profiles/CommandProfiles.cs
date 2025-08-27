using AutoMapper;
using backendapi.Data.Dto.features.Command;
using backendapi.Models;

namespace backendapi.Data.Profiles
{
    public class CommandProfiles : Profile
    {
        public CommandProfiles()
        {   // Source -> Target
            CreateMap<Command, CommandReadDto>();

            CreateMap<CommandCreateDto, Command>();

            CreateMap<CommandUpdateDto, Command>();

            CreateMap<Command, CommandUpdateDto>();

        }
    }
}