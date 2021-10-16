namespace net6.core.Commands
{
    public record CreateBookCommand(string Titulo,
                                    string Autor,
                                    string Genero,
                                    decimal Precio,
                                    uint CantidadPaginas)
        : IRequest<CommandResult>
    { }

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, CommandResult>
    {
        private readonly IMapper _mapper;
        private readonly IPeopleRepository _peopleRepository;

        public CreateBookCommandHandler(IMapper mapper, IPeopleRepository peopleRepository)
        {
            _mapper = mapper;
            _peopleRepository = peopleRepository;
        }

        public async Task<CommandResult> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<People>(request);

            await _peopleRepository.Insert(entity);

            return CommandResult.Success();
        }
    }
}
