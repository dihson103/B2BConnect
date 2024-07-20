using Contract.Services.Branch.Create;
using FluentValidation;

namespace Application.UseCases.Commands.Branches.Create;
public class CreateBranchValidator : AbstractValidator<CreateBranchCommand>
{
    public CreateBranchValidator()
    {
        RuleFor(req => req.Phone)
           .NotEmpty().WithMessage("Hotline không được trống");
        RuleFor(req => req.Email)
           .NotEmpty().WithMessage("Email không được trống");
        RuleFor(req => req.Address)
           .NotEmpty().WithMessage("Địa chỉ chi nhánh không được trống");
    }
}
