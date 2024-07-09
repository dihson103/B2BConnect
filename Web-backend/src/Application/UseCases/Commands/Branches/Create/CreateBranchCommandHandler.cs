using Application.Abstractions.Data;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.Branch.Create;
using Domain.Abstractioins.Exceptions;
using Domain.Entities;
using FluentValidation;

namespace Application.UseCases.Commands.Branches.Create;
public class CreateBranchCommandHandler(IBranchRepository _branchRepository, IUnitOfWork _unitOfWork,
    IValidator<CreateBranchCommand> _validator) : ICommandHandler<CreateBranchCommand>
{
    public async Task<Result.Success> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        await ValidateRequest(request);

        var branch = Branch.Create(request);
        _branchRepository.Add(branch);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success.Create("Tạo chi nhánh thành công");
    }

    private async Task ValidateRequest(CreateBranchCommand request)
    {
        var validatorResult = await _validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            throw new MyValidationException(validatorResult.ToDictionary());
        }
    }
}

