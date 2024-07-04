using Application.Abstractions.Data;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.Industry.SearchIndustries;

namespace Application.UseCases.Queries.Industries.SearchIndustries;
internal sealed class SearchIndustriesQueryHandler(IIndustryRepository _industryRepository)
    : IQueryHandler<SearchIndustriesQuery, List<IndustryResponse>>
{
    public async Task<Result.Success<List<IndustryResponse>>> Handle(
        SearchIndustriesQuery request, 
        CancellationToken cancellationToken)
    {
        var industries = await _industryRepository.SearchIndustrieAsync(request.SearchTerm);
        if (industries is null || industries.Count == 0)
        {
            return Result.Success<List<IndustryResponse>>.Get(null, "Không tìm thấy lĩnh vực nào phù hợp");
        }

        var industryResponses = industries
            .Select(industry => new IndustryResponse(industry.Id, industry.Name))
            .ToList();

        return Result.Success<List<IndustryResponse>>.Get(industryResponses);
    }
}
