using Contract.Services.Branch.Update;
using FluentValidation;

namespace Application.UseCases.Commands.Branches.Update;
public class UpdateBranchValidator : AbstractValidator<UpdateBranchRequest>
{
    public UpdateBranchValidator()
    {
        RuleFor(req => req.Address)
           .NotEmpty().WithMessage("Địa chỉ không được để trống");
    }
}
