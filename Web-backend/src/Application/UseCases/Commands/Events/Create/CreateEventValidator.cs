using Application.Abstractions.Data;
using Contract.Services.Event.Create;
using FluentValidation;

namespace Application.UseCases.Commands.Events.Create;
public class CreateEventValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventValidator(IEventRepository eventRepository)
    {
        RuleFor(req => req.Name)
            .NotEmpty().WithMessage("Tên sự kiện không được để trống")
            .MustAsync(async (name, _) =>
            {
                return ! await eventRepository.IsEventExistAsync(name);
            }).WithMessage("Tên sự kiện không được để trốngsdfsdfsdfsdf");

        RuleFor(req => req.StartAt)
            .Must((startDAte) => startDAte > DateTime.UtcNow)
            .WithMessage("Ngày bắt đầu không được sau ngày hiện tại");
    }
}
