using Contract.Services.Event.Share;
using Contract.Services.Industry.Share;

namespace Contract.Services.Event.GetById;
public record SingleEventResponse(
    Guid Id,
    string Name,
    string? Description,
    DateTime StartAt,
    DateTime EndAt,
    string Location,
    List<ImageResponse> Images,
    EventStatus Status,
    string StatusDescription,
    List<IndustryResponse> Industries);
