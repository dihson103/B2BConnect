using Application.Abstractions.Data;
using AutoMapper;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.Branch.GetBranch;
using Contract.Services.Branch.Share;
using Domain.Abstractioins.Exceptions;

namespace Application.UseCases.Queries.Branches.GetBranch;
public class GetByIdQueryHandler(IBranchRepository _branchRepository, IMapper _mapper) : IQueryHandler<GetByIdQuery, BranchResponse>
{
    public async Task<Result.Success<BranchResponse>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _branchRepository.GetByIdAsync(request.Id)
            ?? throw new MyNotFoundException("Không tìm thấy chi nhánh");

        var branchResponse = _mapper.Map<BranchResponse>(result);

        return Result.Success<BranchResponse>.Get(branchResponse);
    }
}
