using Application.Abstractions.Data;
using AutoMapper;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.Event.GetById;
using Domain.Abstractioins.Exceptions;

namespace Application.UseCases.Queries.Events.GetEvent;
internal class GetByIdQueryHandler(IEventRepository _eventRepository, IMapper _mapper)
    : IQueryHandler<GetByIdQuery, SingleEventResponse>
{
    public async Task<Result.Success<SingleEventResponse>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _eventRepository.GetByIdIncludeIndustriesAsync(request.Id)
            ?? throw new MyNotFoundException("Không tìm thấy event");

        var eventResponse = _mapper.Map<SingleEventResponse>(result);

        return Result.Success<SingleEventResponse>.Get(eventResponse);
    }
}
