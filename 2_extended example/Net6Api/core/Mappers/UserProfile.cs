public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, GetUserResponse>()
        .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.UserName))
        .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.FirstName))
        .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.LastName))
        .ForMember(dest => dest.EmailConfirmado, opt => opt.MapFrom(src => src.EmailConfirmed))
        .ReverseMap();

        CreateMap<RegisterUserCommand, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ReverseMap();
    }

}