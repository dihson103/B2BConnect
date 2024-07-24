using Contract.Abstractions.Messages;
using Contract.Services.Event.Share;

namespace Contract.Services.Event.Create;
public record CreateEventCommand(
    string Name,
    string? Description,
    DateTime StartAt,
    DateTime EndAt,
    string Location,
    List<Guid> IndustryIds,
    List<EventImageRequest>? Images) : ICommand;
