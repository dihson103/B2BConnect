using Application.Abstractions.Data;
using Application.Abstractions.Services;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.Event.GetById;
using Contract.Services.Event.Share;
using Contract.Services.Event.Update;
using Domain.Abstractioins.Exceptions;
using Domain.Entities;
using FluentValidation;

namespace Application.UseCases.Commands.Events.Update;
internal sealed class UpdateEventCommandHandler(
    IEventRepository _eventRepository,
    IMediaRepository _mediaRepository,
    IRequestContext _context,
    IUnitOfWork _unitOfWork,
    IValidator<UpdateEventRequest> _validator) : ICommandHandler<UpdateEventCommand>
{
    public async Task<Result.Success> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var eventt = await GetEventAndValidateRequest(request);

        var updatedBy = _context.UserLoggedIn;

        var medias = new List<Media>();
        var eventMedias = new List<EventMedia>();

        foreach(var imageRequest in request.UpdateEventRequest.ImageRequests)
        {
            var media = Media.Create(imageRequest.image, updatedBy);
            medias.Add(media);
            var eventMedia = EventMedia.Create(eventt.Id, media.Id, imageRequest.isMain);
            eventMedias.Add(eventMedia);
        }
        
        eventt.Update(request.UpdateEventRequest, updatedBy, eventMedias);

        _mediaRepository.AddRange(medias);
        _eventRepository.Update(eventt);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success.Update("Chỉnh sửa sự kiện thành công");
    }

    private async Task<Event> GetEventAndValidateRequest(UpdateEventCommand request)
    {
        var eventt = await _eventRepository.GetByIdAsync(request.Id)
            ?? throw new MyNotFoundException($"Không tìm thấy event với id: {request.Id}");

        if (eventt.Status != EventStatus.PLANNING)
        {
            throw new MyBadRequestException("Do sự kiện đã bắt đầu nên không thể chỉnh sửa sự kiện");
        }

        var validatorResult = await _validator.ValidateAsync(request.UpdateEventRequest);

        return !validatorResult.IsValid ? 
            throw new MyValidationException(validatorResult.ToDictionary()) : eventt;
    }
}
