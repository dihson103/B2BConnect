using Application.Abstractions.Data;
using AutoMapper;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Dtos.Search;
using Contract.Abstractions.Messages;
using Contract.Services.Business.GetBusinesses;
using Contract.Services.Business.Share;

namespace Application.UseCases.Queries.Businesses.GetBusinesses;
public sealed class GetBusinessQueryHandler(IBusinessRepository _businessRepository, IMapper _mapper) :
    IQueryHandler<GetBusinessesQuery, SearchResponse<List<BusinessesResponse>>>
{
    public async Task<Result.Success<SearchResponse<List<BusinessesResponse>>>> Handle(GetBusinessesQuery request, 
        CancellationToken cancellationToken)
    {

        var result = await _businessRepository.SearchBusinessAsync(request);

        var businesses = result.Item1;
        var totalPage = result.Item2;
        var totalItems = result.Item3;

        if (businesses is null || businesses.Count <= 0 || totalPage <= 0)
        {
            return Result.Success<SearchResponse<List<BusinessesResponse>>>
               .Get(null, "Không tìm thấy doanh nghiệp nào phù hợp");
        }

        var data = businesses.ConvertAll(p => _mapper.Map<BusinessesResponse>(p));

        var searchResponse = new SearchResponse<List<BusinessesResponse>>(request.PageIndex, totalPage, totalItems, data);

        return Result.Success<SearchResponse<List<BusinessesResponse>>>.Get(searchResponse);
    }
}
