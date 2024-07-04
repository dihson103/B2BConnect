using AutoMapper;
using Contract.Services.Event.GetEvents;
using Contract.Services.Event.Share;
using Domain.Entities;

namespace Application.Mapper;
public class EventMappingProfile : Profile
{
    public EventMappingProfile()
    {
        CreateMap<Event, EventResponse>()
            .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
            .ForCtorParam("Name", opt => opt.MapFrom(src => src.Name))
            .ForCtorParam("Description", opt => opt.MapFrom(src => src.Description))
            .ForCtorParam("Location", opt => opt.MapFrom(src => src.Location))
            .ForCtorParam("Image", opt => opt.MapFrom(src => src.Image))
            .ForCtorParam("Status", opt => opt.MapFrom(src => src.Status))
            .ForCtorParam("StatusDescription", opt => opt.MapFrom(src => src.Status.GetDescription()));
    }
}
