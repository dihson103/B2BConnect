using Application.Abstractions.Data;
using Application.Abstractions.Services;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.Verifications.Update;
using Domain.Abstractioins.Exceptions;
using FluentValidation;

namespace Application.UseCases.Commands.Verifications.Update;
public class UpdateVerificationCommandHandler(IVerificationRepository _verificationRepository,
    IRequestContext _requestContext, IUnitOfWork _unitOfWork, IValidator<UpdateVerificationCommand> _validator)
     : ICommandHandler<UpdateVerificationCommand>
{
    public async Task<Result.Success> Handle(UpdateVerificationCommand request, CancellationToken cancellationToken)
    {
        await ValidateRequest(request);
        var verId = request.VerificationId;
        var loggedUser = _requestContext.UserLoggedIn;

        var ve = await _verificationRepository.GetById(verId) ??
            throw new MyNotFoundException("Không tìm thấy doanh nghiệp cần xác thực");

        ve.UpdatedDate = DateTime.UtcNow;
        ve.IsChecked = true;
        ve.CheckedDate = DateTime.UtcNow;
        ve.UpdatedBy = loggedUser;

        _verificationRepository.Update(ve);
        return Result.Success.Create("Xác thực doanh nghiệp thành công!");
    }

    private async Task ValidateRequest(UpdateVerificationCommand request)
    {
        var validatorResult = await _validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            throw new MyValidationException(validatorResult.ToDictionary());
        }
    }
}
