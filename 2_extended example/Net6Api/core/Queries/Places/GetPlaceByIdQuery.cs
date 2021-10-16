﻿namespace net6.core.Queries.Place
{
    public record GetPlaceByIdQuery(Guid Id) : IRequest<GetPlaceResponse?> { }


    public class GetPlaceByIdQueryHandler : IRequestHandler<GetPlaceByIdQuery, GetPlaceResponse?>
    {
        private readonly IMapper _mapper;
        private readonly IPlaceRepository _placeRepository;

        public GetPlaceByIdQueryHandler(IMapper mapper, IPlaceRepository PlaceRepository)
        {
            _mapper = mapper;
            _placeRepository = PlaceRepository;
        }

        public async Task<GetPlaceResponse?> Handle(GetPlaceByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _placeRepository.GetById(request.Id);

            if (result is null)
                return default;

            return _mapper.Map<GetPlaceResponse>(result);
        }
    }
}
