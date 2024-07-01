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
    IUnitOfWork _unitOfWork,
    IValidator<CreateEventCommand> _validator) : ICommandHandler<CreateEventCommand>
{
    public async Task<Result.Success> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            throw new MyValidationException(validatorResult.ToDictionary());
        }

        var eventt = Event.Create(request);

        _eventRepository.Add(eventt);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success.Create("Tạo sự kiện thành công");
    }
}
