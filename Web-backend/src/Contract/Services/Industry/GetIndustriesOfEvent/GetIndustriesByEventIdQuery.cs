using Contract.Abstractions.Messages;
using Contract.Services.Industry.Share;

namespace Contract.Services.Industry.GetIndustriesOfEvent;
public record GetIndustriesByEventIdQuery(Guid eventId) : IQuery<List<IndustryResponse>>;
