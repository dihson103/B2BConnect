using Application.Abstractions.Data;
using AutoMapper;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Dtos.Search;
using Contract.Abstractions.Messages;
using Contract.Services.Event.GetEvents;

namespace Application.UseCases.Queries.Events.GetEvents;
internal sealed class GetEventsQueryHandler(IEventRepository _eventRepository, IMapper _mapper)
    : IQueryHandler<GetEventsQuery, SearchResponse<List<EventResponse>>>
{
    public async Task<Result.Success<SearchResponse<List<EventResponse>>>> Handle(
        GetEventsQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _eventRepository.SearchEventsAsync(request);
        var events = result.Item1;
        var totalPages = result.Item2;
        var totalItems = result.Item3;

        if (events is null || events.Count == 0)
        {
            return Result.Success<SearchResponse<List<EventResponse>>>
                .Get(null, "Không tìm thấy sự kiện nào phù hợp");
        }

        var eventResponses = events.Select(e => _mapper.Map<EventResponse>(e)).ToList();
        var searchResponse = new SearchResponse<List<EventResponse>>(request.PageIndex, totalPages, totalItems, eventResponses);
        return Result.Success<SearchResponse<List<EventResponse>>>.Get(searchResponse);

    }
}
