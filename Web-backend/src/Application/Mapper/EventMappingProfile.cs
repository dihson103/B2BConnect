using AutoMapper;
using Contract.Services.Event.GetById;
using Contract.Services.Event.GetEvents;
using Contract.Services.Event.Share;
using Contract.Services.Industry.Share;
using Domain.Entities;

namespace Application.Mapper;
public class EventMappingProfile : Profile
{
    public EventMappingProfile()
    {
        CreateMap<Event, EventResponse>()
            .ConstructUsing(src => new EventResponse(
                src.Id,
                src.Name,
                src.Description,
                src.StartAt,
                src.EndAt,
                src.Location,
                src.EventMedias.FirstOrDefault(sm => sm.IsMain) == null ? "image_not_found.png" : src.EventMedias.FirstOrDefault(sm => sm.IsMain).Media.Path,
                src.Status,
                src.Status.GetDescription()
                ));

        CreateMap<Event, SingleEventResponse>()
            .ConstructUsing(src => new SingleEventResponse(
                src.Id,
                src.Name,
                src.Description,
                src.StartAt,
                src.EndAt,
                src.Location,
                src.EventMedias != null && src.EventMedias.Count > 0
                    ? src.EventMedias.Select(em => new ImageResponse(em.MediaId, em.Media.Path, em.IsMain)).ToList()
                    : new List<ImageResponse>(),
                src.Status,
                src.Status.GetDescription(),
                src.EventIndustries != null
                    ? src.EventIndustries.Select(x => new IndustryResponse(x.IndustryId, x.Industry.Name)).ToList()
                    : new List<IndustryResponse>()
            ));

    }
}
