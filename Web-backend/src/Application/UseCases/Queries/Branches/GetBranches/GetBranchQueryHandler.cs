using Application.Abstractions.Data;
using AutoMapper;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Dtos.Search;
using Contract.Abstractions.Messages;
using Contract.Services.Branch.GetBranches;
using Contract.Services.Branch.Share;


namespace Application.UseCases.Queries.Branches.GetBranches;
public sealed class GetBranchQueryHandler(IBranchRepository _branchRepository, IMapper _mapper) :
    IQueryHandler<GetBranchesQuery, SearchResponse<List<BranchResponse>>>
{
    public async Task<Result.Success<SearchResponse<List<BranchResponse>>>> Handle(GetBranchesQuery request, CancellationToken cancellationToken)
    {
        var result = await _branchRepository.GetBranchesAsync(request);

        if (result is null || result.Count <= 0 )
        {
            return Result.Success<SearchResponse<List<BranchResponse>>>
               .Get(null, "Doanh nghiệp chưa có chi nhánh");
        }

        var data = result.ConvertAll(p => _mapper.Map<BranchResponse>(p));

        var searchResponse = new SearchResponse<List<BranchResponse>>(0, 0, 0,data);

        return Result.Success<SearchResponse<List<BranchResponse>>>.Get(searchResponse);
    }
}
