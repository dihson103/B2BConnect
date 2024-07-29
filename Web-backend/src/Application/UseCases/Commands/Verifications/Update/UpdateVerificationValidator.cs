using Contract.Services.Verifications.Update;
using FluentValidation;

namespace Application.UseCases.Commands.Verifications.Update;
public class UpdateVerificationValidator : AbstractValidator<UpdateVerificationCommand>
{
    public UpdateVerificationValidator()
    {
        RuleFor(req => req.VerificationId)
         .NotEmpty().WithMessage("Verification Id không được trống");
    }
}
