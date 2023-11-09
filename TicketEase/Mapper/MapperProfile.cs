using AutoMapper;
using TicketEase.Application.DTO;
using TicketEase.Domain.Entities;

namespace TicketEase.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Manager, EditManagerDto>().ReverseMap();
            CreateMap<BoardRequestDto, Board>();
            CreateMap<Board, BoardResponseDto>().ReverseMap();
        }
    }
}
