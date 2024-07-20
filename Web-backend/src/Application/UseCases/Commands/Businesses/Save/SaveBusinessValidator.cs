using Contract.Services.Business.Create;
using FluentValidation;
namespace Application.UseCases.Commands.Businesses.Create;
public class SaveBusinessValidator : AbstractValidator<SaveBusinessCommand>
{
    public SaveBusinessValidator()
    {
        RuleFor(req => req.TaxCode)
           .NotEmpty().WithMessage("Mã số thuế không được trống");

        RuleFor(req => req.Name)
          .NotEmpty().WithMessage("Tên doanh nghiệp không được trống");

        RuleFor(req => req.DateOfEstablishments)
          .NotEmpty().WithMessage("Ngày thành lập không được trống")
        .Must(date => date < DateOnly.FromDateTime(DateTime.Now))
              .WithMessage("Ngày thành lập phải là ngày trong quá khứ");

        RuleFor(req => req.IndustryIds)
          .NotEmpty().WithMessage("Doanh nghiệp phải có lĩnh vực");

        RuleFor(req => req.Branches)
         .NotEmpty().WithMessage("Doanh nghiệp phải có tối thiếu 1 địa chỉ");
    }
}
