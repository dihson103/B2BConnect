using Contract.Services.Business.Create;
using FluentValidation;
namespace Application.UseCases.Commands.Businesses;
public class CreateBusinessValidator : AbstractValidator<CreateBusinessCommand>
{
    public CreateBusinessValidator()
    {
        RuleFor(req => req.TaxCode)
           .NotEmpty().WithMessage("Mã số thuế không được trống");
    }
}
