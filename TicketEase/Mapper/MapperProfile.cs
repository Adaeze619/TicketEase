﻿using AutoMapper;
using TicketEase.Application.DTO;
using TicketEase.Domain.Entities;

namespace TicketEase.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<BoardRequestDto, Board>();
            CreateMap<Board, BoardResponseDto>().ReverseMap();
            CreateMap<Ticket, TicketDto>().ReverseMap();
            CreateMap<UpdateTicketDto, Ticket>();
        }
    }
}
