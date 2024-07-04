using System.Diagnostics.Tracing;
using Application.Abstractions.Data;
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
    IUnitOfWork _unitOfWork,
    IValidator<CreateEventCommand> _validator) : ICommandHandler<CreateEventCommand>
{
    public async Task<Result.Success> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        await ValidateRequestAndGetIndustry(request);

        var eventt = Event.Create(request);
        var eventIndustries = request.IndustryIds
            .Select(industryId => EventIndustry.Create(eventt.Id, industryId)).ToList();

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
