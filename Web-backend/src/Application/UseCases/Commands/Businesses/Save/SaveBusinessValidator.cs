using Contract.Services.Business.Create;
using Domain.Abstractioins.Exceptions;
using FluentValidation;
using MediatR;
namespace Application.UseCases.Commands.Businesses.Create;
public class SaveBusinessValidator : AbstractValidator<SaveBusinessCommand>
{
    public SaveBusinessValidator()
    {
       
        RuleFor(req => req.accountId).NotEmpty().WithMessage("Account Id không được rỗng");

        RuleFor(req => req.TaxCode)
           .NotEmpty().WithMessage("Mã số thuế không được trống");

        RuleFor(req => req.Name)
          .NotEmpty().WithMessage("Tên doanh nghiệp không được trống");

        RuleFor(req => req.DateOfEstablishments)
          .NotEmpty().WithMessage("Ngày thành lập không được trống")
        .Must(date => date < DateOnly.FromDateTime(DateTime.Now))
              .WithMessage("Ngày thành lập không phải là ngày của tương lai");

        RuleFor(req => req.IndustryIds)
          .NotEmpty().WithMessage("Doanh nghiệp phải có lĩnh vực");

        RuleFor(req => req.Branches)
         .NotEmpty().WithMessage("Doanh nghiệp phải có tối thiếu 1 địa chỉ");
    }
}
