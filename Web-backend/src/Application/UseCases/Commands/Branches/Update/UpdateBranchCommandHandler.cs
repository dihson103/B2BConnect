
using Application.Abstractions.Data;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.Branch.Update;
using Contract.Services.Event.Share;
using Contract.Services.Event.Update;
using Domain.Abstractioins.Exceptions;
using Domain.Entities;
using FluentValidation;

namespace Application.UseCases.Commands.Branches.Update;
public sealed class UpdateBranchCommandHandler(IBranchRepository _branchRepository,
    IUnitOfWork _unitOfWork, IValidator<UpdateBranchRequest> _validator) : ICommandHandler<UpdateBranchCommand>
{
    public async Task<Result.Success> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = await GetBranchAndValidateRequest(request);
        branch.Update(request.UpdateBranchRequest);
        _branchRepository.Update(branch);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success.Update("Chỉnh sửa chi nhánh thành công");
    }

    private async Task<Branch> GetBranchAndValidateRequest(UpdateBranchCommand request)
    {
        var branch = await _branchRepository.GetByIdAsync(request.Id)
            ?? throw new MyNotFoundException($"Không tìm thấy chi nhánh với id: {request.Id}");

        var validatorResult = await _validator.ValidateAsync(request.UpdateBranchRequest);

        return !validatorResult.IsValid ?
            throw new MyValidationException(validatorResult.ToDictionary()) : branch;
    }
}
