using Application.Abstractions.Data;
using Application.Abstractions.Services;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.Branch.Create;
using Contract.Services.Verifications.Create;
using Domain.Abstractioins.Exceptions;
using Domain.Entities;
using FluentValidation;

namespace Application.UseCases.Commands.Verifications.Create;
public class CreateVerificationCommandHandler(IVerificationRepository _verificationRepository, IUnitOfWork _unitOfWork,
    IBusinessRepository _businessRepository, IBranchRepository _branchRepository, IRequestContext _context,
    IRepresentativeRepository _representativeRepository, IValidator<CreateVerificationCommand> _validator)
    : ICommandHandler<CreateVerificationCommand>
{
    public async Task<Result.Success> Handle(CreateVerificationCommand request, CancellationToken cancellationToken)
    {
        await ValidateRequest(request);

        Guid businessId = request.BusinessId;

        // Cập nhật doanh nghiệp
        var business = await _businessRepository.GetByIdAsync(businessId);
        if(business != null)
        {
            var re = await _representativeRepository.GetByBusinessId(businessId);
            if (re != null)
            {
                re.Fullname = request.representativeName;
                re.GovernmentId = request.govermentId;
                _representativeRepository.Update(re);  
            }
           
            business.TaxCode = request.taxNumber;
            business.Name = request.name;
            business.WebSite = request.website;
            business.DateOfEstablishment = request.dateOfEstablishment;
            business.NumberOfEmployee = request.NumberOfEmployee;
            _businessRepository.Update(business);

            // Xóa chi nhánh chính
            _branchRepository.DeleteMainBranchOfBusiness(businessId);
            var branchCommand = new CreateBranchCommand(request.email, request.phone, request.address, true);
            var branch = Branch.Create(branchCommand);
            _branchRepository.Add(branch);

            // Tạo và lưu xác thực
            Verification verification = Verification.Create(request);
            verification.CreatedDate = DateTime.UtcNow;
            verification.CreatedBy = _context.UserLoggedIn;
            _verificationRepository.Add(verification);

            await _unitOfWork.SaveChangesAsync();
            return Result.Success.Create("Gửi mẫu xác thực thành công");
        }
        return Result.Success.Create("Doanh nghiệp không tồn tại");
    }

    private async Task ValidateRequest(CreateVerificationCommand request)
    {
        var validatorResult = await _validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            throw new MyValidationException(validatorResult.ToDictionary());
        }
    }
}

