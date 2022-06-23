using AutoMapper;
using DevChatAPI2.Models;
using DevChatAPI2.Responses;

namespace DevChatAPI2.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RoomChat, RoomResponse>();
            CreateMap<Message, MessageResponse>();
        }
    }
}
