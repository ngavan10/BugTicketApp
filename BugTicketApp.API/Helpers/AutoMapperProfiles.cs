using System.Linq;
using AutoMapper;
using BugTicketApp.API.Dtos;
using BugTicketApp.API.Models;

namespace BugTicketApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Ticket, TicketForListDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
            CreateMap<TicketForUpdateDto, Ticket>();
            CreateMap<CommentForCreationDto, Comment>().ReverseMap();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<Comment, CommentForReturnDto>().ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName));;
            //CreateMap<Comment, CommentForReturnDto>();
            CreateMap<User, UserForReturnDto>();
            CreateMap<TicketForCreationDto, Ticket>();
        }
    }
}