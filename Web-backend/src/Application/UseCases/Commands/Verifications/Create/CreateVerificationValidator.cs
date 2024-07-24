using Contract.Services.Verifications.Create;
using FluentValidation;

namespace Application.UseCases.Commands.Verifications.Create;
public class CreateVerificationValidator : AbstractValidator<CreateVerificationCommand>
{
    public CreateVerificationValidator()
    {
        RuleFor(req => req.phone)
          .NotEmpty().WithMessage("Hotline doanh nghiệp không được trống");
        RuleFor(req => req.email)
           .NotEmpty().WithMessage("Email doanh nghiệp không được trống");
        RuleFor(req => req.address)
           .NotEmpty().WithMessage("Địa chỉ doanh nghiệp không được trống");

        RuleFor(req => req.taxNumber)
          .NotEmpty().WithMessage("Mã số thuế không được trống");
        RuleFor(req => req.businessLicense)
        .NotEmpty().WithMessage("Bạn phải tải lên giấy phép kinh doanh (dạng PDF)");

        RuleFor(req => req.businessType)
       .NotEmpty().WithMessage("Loại hình doanh nghiệp không được trống");

        RuleFor(req => req.establishmentCertificate)
      .NotEmpty().WithMessage("Bạn phải tải lên chứng chỉ thành lập để xác thực (PDF)");

        RuleFor(req => req.dateOfEstablishment)
         .NotEmpty().WithMessage("Ngày thành lập không được trống")
       .Must(date => date < DateOnly.FromDateTime(DateTime.Now))
             .WithMessage("Ngày thành lập không phải là ngày của tương lai");

        RuleFor(req => req.representativeName)
     .NotEmpty().WithMessage("Tên người đại diện không được trống");

        RuleFor(req => req.govermentId)
    .NotEmpty().WithMessage("Số CCCD của người đại diện không được trống");
    }
}
