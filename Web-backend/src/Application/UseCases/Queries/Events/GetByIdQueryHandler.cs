using Application.Abstractions.Data;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.Event.GetById;
using Domain.Abstractioins.Exceptions;

namespace Application.UseCases.Queries.Events;
internal class GetByIdQueryHandler(IEventRepository _eventRepository) 
    : IQueryHandler<GetByIdQuery, EventResponse>
{
    public async Task<Result.Success<EventResponse>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _eventRepository.GetByIdAsync(request.Id)
            ?? throw new MyNotFoundException("Không tìm thấy event");

        var eventResponse = new EventResponse(result.Id, result.Name, result.Description);

        return Result.Success<EventResponse>.Get(eventResponse);
    }
}
