using Application.Abstractions.Data;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.Industry.GetIndustriesOfEvent;
using Contract.Services.Industry.Share;

namespace Application.UseCases.Queries.Industries.GetIndustriesByEventId;
internal sealed class GetIndustriesByEventIdQueryHandler(IEventIndustryRepository _eventIndustryRepository)
    : IQueryHandler<GetIndustriesByEventIdQuery, List<IndustryResponse>>
{
    public async Task<Result.Success<List<IndustryResponse>>> Handle(GetIndustriesByEventIdQuery request, CancellationToken cancellationToken)
    {
        var eventIndustries = await _eventIndustryRepository.GetByEventIdAsync(request.eventId);
        if(eventIndustries == null || eventIndustries.Count == 0)
        {
            return Result.Success<List<IndustryResponse>>.Get(new List<IndustryResponse>());
        }

        var industryResponses = eventIndustries
            .Select(eventIndustry => new IndustryResponse(eventIndustry.Industry.Id, eventIndustry.Industry.Name))
            .ToList();

        return Result.Success<List<IndustryResponse>>.Get(industryResponses);
    }
}
