using AutoMapper;
using TicketEase.Application.DTO;
using TicketEase.Domain.Entities;

using AutoMapper;
using TicketEase.Application.DTO.Project;
using TicketEase.Domain.Entities;

namespace TicketEase.Mapper
{ 
   
        
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ProjectCreationDto, Project>()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.BoardId, opt => opt.Ignore());

            CreateMap<ProjectUpdateDto, Project>();

            CreateMap<Project, ProjectDto>();
            CreateMap<BoardRequestDto, Board>();
            CreateMap<Board, BoardResponseDto>().ReverseMap();
        }
    }
}
