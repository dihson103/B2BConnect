using Contract.Services.Event.Share;

namespace Contract.Services.Event.Update;
public record UpdateEventRequest(
    string Name,
    string Description,
    DateTime StartAt,
    DateTime EndAt,
    string Location,
    string Image,
    List<Guid> IndustryIds,
    EventStatus Status);


