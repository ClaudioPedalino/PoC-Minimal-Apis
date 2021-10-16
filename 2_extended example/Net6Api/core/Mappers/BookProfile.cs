public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, GetBookResponse>()
              .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Title))
              .ForMember(dest => dest.Autor, opt => opt.MapFrom(src => src.Author))
              .ForMember(dest => dest.Genero, opt => opt.MapFrom(src => src.Genre))
              .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => src.Price))
              .ForMember(dest => dest.CantidadPaginas, opt => opt.MapFrom(src => src.TotalPages))
              .ReverseMap();

        CreateMap<CreateBookCommand, Book>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Titulo))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Autor))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genero))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Precio))
            .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(src => src.CantidadPaginas))
            .ReverseMap();

        CreateMap<UpdateBookCommand, Book>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Titulo))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Autor))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genero))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Precio))
            .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(src => src.CantidadPaginas))
            .ReverseMap();
    }
}