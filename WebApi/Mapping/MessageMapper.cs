using AutoMapper;
using WebApi.DTOs;
using WebApi.Models;

namespace WebApi.Mapping;

public class MessageMapper : Profile
{
    public MessageMapper()
    {
        CreateMap<Message, MessageDto>()
            .ReverseMap();

        CreateMap<Message, CreateMessageDto>()
            .ReverseMap();
    }
}
