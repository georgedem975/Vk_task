using AutoMapper;
using Core.DTOs.Incoming;
using Core.DTOs.Outgoing;
using Core.Services.Hash;
using DataAccess.Models;

namespace Core.Mappers;

public class AppMappingProfile : Profile
{
    private IHashService _hashService = new HashService();
    
    public AppMappingProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.UserGroupCode,
                opt => opt.MapFrom(src => src.UserGroup.Code))
            .ForMember(dest => dest.UserGroupDescription,
                opt => opt.MapFrom(src => src.UserGroup.Description))
            .ForMember(dest => dest.UserStateCode,
                opt => opt.MapFrom(src => src.UserState.Code))
            .ForMember(dest => dest.UserStateDescription,
                opt => opt.MapFrom(src => src.UserState.Description));

        CreateMap<UserForCreationDto, User>()
            .ForMember(dest => dest.UserState,
                opt => opt.MapFrom(src => new UserState("Active", src.UserStateDescription)))
            .ForMember(dest => dest.UserGroup,
                opt => opt.MapFrom(src => new UserGroup("User", src.UserGroupDescription)))
            .ForMember(dest => dest.CreatedDate,
                opt => opt.MapFrom(src => DateTime.Now.ToUniversalTime()))
            .ForMember(dest => dest.Password,
                opt => opt.MapFrom(src => _hashService.GetHashPassword(src.Password)));
        
        CreateMap<AdminForCreationDto, User>()
            .ForMember(dest => dest.UserState,
                opt => opt.MapFrom(src => new UserState("Active", src.UserStateDescription)))
            .ForMember(dest => dest.UserGroup,
                opt => opt.MapFrom(src => new UserGroup("Admin", src.UserGroupDescription)))
            .ForMember(dest => dest.CreatedDate,
                opt => opt.MapFrom(src => DateTime.Now.ToUniversalTime()))
            .ForMember(dest => dest.Password,
                opt => opt.MapFrom(src => _hashService.GetHashPassword(src.Password)));

        CreateMap<User, DeletedUserDto>()
            .ForMember(dest => dest.Login,
                opt => opt.MapFrom(src => src.Login))
            .ForMember(dest => dest.UserStatusCode,
                opt => opt.MapFrom(src => src.UserState.Code));
    }
}