namespace net6.core.Mappers
{
    internal class PlaceProfile : Profile
    {
        public PlaceProfile()
        {
            CreateMap<Place, GetPlaceResponse>()
                  .ForMember(dest => dest.Ciudad, opt => opt.MapFrom(src => src.City))
                  .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Address))
                  .ForMember(dest => dest.Numeracion, opt => opt.MapFrom(src => src.Numeration))
                  .ForMember(dest => dest.Latitud, opt => opt.MapFrom(src => src.Latitude))
                  .ForMember(dest => dest.Longitud, opt => opt.MapFrom(src => src.Longitude))
                  .ReverseMap();

            CreateMap<CreatePlaceCommand, Place>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Ciudad))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Direccion))
                .ForMember(dest => dest.Numeration, opt => opt.MapFrom(src => src.Numeracion))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitud))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitud))
                .ReverseMap();

            CreateMap<UpdatePlaceCommand, Place>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Ciudad))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Direccion))
                .ForMember(dest => dest.Numeration, opt => opt.MapFrom(src => src.Numeracion))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitud))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitud))
                .ReverseMap();
        }
    }
}
