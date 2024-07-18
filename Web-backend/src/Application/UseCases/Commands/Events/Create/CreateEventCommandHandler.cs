using Application.Abstractions.Data;
using Application.Abstractions.Services;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.Event.Create;
using Domain.Abstractioins.Exceptions;
using Domain.Entities;
using FluentValidation;

namespace Application.UseCases.Commands.Events.Create;
public class CreateEventCommandHandler(
    IEventRepository _eventRepository,
    IEventIndustryRepository _eventIndustryRepository,
    IMediaRepository _mediaRepository,
    IEventMediaRepository _eventMediaRepository,
    IRequestContext _context,
    IUnitOfWork _unitOfWork,
    IValidator<CreateEventCommand> _validator) : ICommandHandler<CreateEventCommand>
{
    public async Task<Result.Success> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        await ValidateRequestAndGetIndustry(request);

        var createdBy = _context.UserLoggedIn;

        var eventt = Event.Create(request, createdBy);
        var eventIndustries = request.IndustryIds
            .Select(industryId => EventIndustry.Create(eventt.Id, industryId)).ToList();

        if(request.Images is not null && request.Images.Count > 0)
        {
            var medias = new List<Media>();
            var eventMedias = new List<EventMedia>();

            foreach (var image in request.Images)
            {
                var media = Media.Create(image.image, createdBy);
                var eventMedia = EventMedia.Create(eventt.Id, media.Id, image.isMain);
                medias.Add(media);
                eventMedias.Add(eventMedia);
            }

            _mediaRepository.AddRange(medias);
            _eventMediaRepository.AddRange(eventMedias);
        }

        _eventRepository.Add(eventt);
        _eventIndustryRepository.AddRange(eventIndustries);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success.Create("Tạo sự kiện thành công");
    }

    private async Task ValidateRequestAndGetIndustry(CreateEventCommand request)
    {
        var validatorResult = await _validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            throw new MyValidationException(validatorResult.ToDictionary());
        }
    }
}
