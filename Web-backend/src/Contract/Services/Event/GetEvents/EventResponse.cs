using Contract.Services.Event.Share;

namespace Contract.Services.Event.GetEvents;
public record EventResponse(
    Guid Id,
    string Name,
    string? Description,
    DateTime StartAt,
    DateTime EndAt,
    string Location,
    string Image,
    EventStatus Status,
    string StatusDescription);
