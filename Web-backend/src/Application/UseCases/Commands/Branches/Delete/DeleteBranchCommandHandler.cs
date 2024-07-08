using Application.Abstractions.Data;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.Branch.Delete;

namespace Application.UseCases.Commands.Branches.Delete;
public class DeleteBranchCommandHandler(IBranchRepository _branchRepository, IUnitOfWork _unitOfWork) : ICommandHandler<DeleteBranchCommand>
{
    public async Task<Result.Success> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = await _branchRepository.GetByIdAsync(request.id) ?? throw new Domain.Abstractioins.Exceptions.MyNotFoundException("Không tìm thấy chi nhánh cần xóa!");

        _branchRepository.Delete(branch);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success.Create("Xóa chi nhánh thành công");
    }
}
