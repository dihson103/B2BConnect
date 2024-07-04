using Contract.Abstractions.Dtos.Search;
using Contract.Abstractions.Messages;
using Contract.Services.Event.Share;

namespace Contract.Services.Event.GetEvents;
public record GetEventsQuery(
    string? SearchTerm,
    EventStatus Status,
    int PageIndex = 1,
    int PageSize = 10) : IQuery<SearchResponse<List<EventResponse>>>;
