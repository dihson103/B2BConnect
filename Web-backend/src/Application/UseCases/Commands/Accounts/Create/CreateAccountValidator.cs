using Application.Abstractions.Data;
using Application.Utils;
using Contract.Services.Account.Create;
using Contract.Services.Event.Create;
using FluentValidation;

namespace Application.UseCases.Commands.Accounts.Create;
public class CreateAccountValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountValidator()
    {
        RuleFor(req => req.Email)
            .NotEmpty().WithMessage("Email không được để trống");

        RuleFor(req => req.Password)
            .NotEmpty().WithMessage("Password không được để trống");

        RuleFor(req => req.Email)
            .EmailAddress().WithMessage("Email không đúng định dạng.");
    }
}
