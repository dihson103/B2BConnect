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

        RuleFor(req => req.ImageRequests)
            .Must((requests) => requests != null && requests.Count > 0).WithMessage("Sự kiện phải có ảnh")
            .Must((requests) => requests.Count(req => req.isMain) == 1).WithMessage("Sự kiện phải có duy nhất ảnh chính");

        RuleFor(req => req.StartAt)
            .Must((startDate) => DateUtil.FromDateTimeClientToDateTimeUtc(startDate) > DateTime.UtcNow)
            .WithMessage("Ngày bắt đầu phải sau ngày hiện tại");

        RuleFor(req => req.EndAt)
            .Must((req, endDate) => endDate > req.StartAt)
            .WithMessage("Ngày kết thúc phải sau ngày bắt đầu");

        RuleFor(req => req.IndustryIds)
            .Must((industryIds) => industryIds != null && industryIds.Count() > 0)
            .WithMessage("Phải có ít nhất 1 lĩnh vực trong sự kiện")
            .MustAsync(async (industryIds, _) => await industryRepository.IsAllIndustryIdsExistAsync(industryIds))
            .WithMessage("Có một vài lĩnh vực không tồn tại");
    }
}
