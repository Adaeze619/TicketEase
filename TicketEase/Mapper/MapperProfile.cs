using AutoMapper;
using TicketEase.Application.DTO;
using TicketEase.Common.Utilities;
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
            CreateMap<ProjectRequestDto, Project>()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.BoardId, opt => opt.Ignore());

            CreateMap<UpdateProjectRequestDto, Project>();

            CreateMap<Project, ProjectReponseDto>().ReverseMap();
            CreateMap<Manager, EditManagerDto>().ReverseMap();
            CreateMap<BoardRequestDto, Board>();
            CreateMap<Board, BoardResponseDto>().ReverseMap();
<<<<<<< Updated upstream
            CreateMap<Ticket, TicketDto>().ReverseMap();
            CreateMap<UpdateTicketDto, Ticket>();
=======
            CreateMap<Ticket, TicketResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) // Map other properties accordingly
            .ForMember(dest => dest.TicketReference, opt => opt.MapFrom(src => src.TicketReference))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment));
            CreateMap<Ticket, TicketRequestDto>().ReverseMap();
            CreateMap<UpdateTicketRequestDto, Ticket>();
>>>>>>> Stashed changes
            CreateMap<AppUser, AppUserDto>();
            CreateMap<PageResult<IEnumerable<AppUser>>, PageResult<IEnumerable<AppUserDto>>>();
            CreateMap<UpdateUserDto, AppUser>();
        }
    }
}
