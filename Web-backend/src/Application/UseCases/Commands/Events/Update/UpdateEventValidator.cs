using Application.Abstractions.Data;
using Application.Utils;
using Contract.Services.Event.Update;
using FluentValidation;

namespace Application.UseCases.Commands.Events.Update;
public class UpdateEventValidator : AbstractValidator<UpdateEventRequest>
{
    public UpdateEventValidator(IIndustryRepository industryRepository)
    {
        RuleFor(req => req.Name)
            .NotEmpty().WithMessage("Tên sự kiện không được để trống");

        RuleFor(req => req.Location)
            .NotEmpty().WithMessage("Địa điểm tổ chức không được để trống");

        RuleFor(req => req.Image)
            .NotEmpty().WithMessage("Sự kiện phải có ảnh");

        RuleFor(req => req.StartAt)
            .Must((startDate) => DateUtil.FromDateTimeClientToDateTimeUtc(startDate) > DateTime.UtcNow)
            .WithMessage("Ngày bắt đầu phải sau ngày hiện tại");

        RuleFor(req => req.EndAt)
            .Must((req, endDate) => endDate > req.StartAt)
            .WithMessage("Ngày bắt đầu phải sau ngày hiện tại");

        RuleFor(req => req.IndustryIds)
            .Must((industryIds) => industryIds != null && industryIds.Count() > 0)
            .WithMessage("Phải có ít nhất 1 lĩnh vực trong sự kiện")
            .MustAsync(async (industryIds, _) => await industryRepository.IsAllIndustryIdsExistAsync(industryIds))
            .WithMessage("Có một vài lĩnh vực không tồn tại");
    }
}
