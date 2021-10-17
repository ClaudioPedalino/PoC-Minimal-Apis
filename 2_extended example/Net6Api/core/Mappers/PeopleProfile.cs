public class PeopleProfile : Profile
{
    public PeopleProfile()
    {
        CreateMap<People, GetPeopleResponse>()
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Edad, opt => opt.MapFrom(src => src.Age))
            .ForMember(dest => dest.FechaNacimiento, opt => opt.MapFrom(src => src.Birthday))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.AvatarUrl))
            .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Phone))
            .ReverseMap();

        CreateMap<CreatePeopleCommand, People>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Apellido))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Edad))
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.FechaNacimiento))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.AvatarUrl))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Telefono))
            .ReverseMap();

        CreateMap<UpdatePeopleCommand, People>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Apellido))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Edad))
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.FechaNacimiento))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.AvatarUrl))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Telefono))
            .ReverseMap();
    }
}