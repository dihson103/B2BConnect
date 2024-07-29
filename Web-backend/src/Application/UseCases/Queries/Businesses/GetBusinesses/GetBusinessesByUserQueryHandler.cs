using Application.Abstractions.Data;
using AutoMapper;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Dtos.Search;
using Contract.Abstractions.Messages;
using Contract.Services.Business.GetBusinesses;
using Contract.Services.Business.Share;
using Domain.Abstractioins.Exceptions;

namespace Application.UseCases.Queries.Businesses.GetBusinesses;
public sealed class GetBusinessesByUserQueryHandler(IBusinessRepository _businessRepository, IMapper _mapper) :
    IQueryHandler<GetBusinessesByUserQuery, SearchResponse<List<BusinessesResponse>>>
{
    public async Task<Result.Success<SearchResponse<List<BusinessesResponse>>>> Handle(GetBusinessesByUserQuery request,
        CancellationToken cancellationToken)
    {

        List<Guid>? parsedIndustryIds = null;
        if (!string.IsNullOrEmpty(request.IndustryIds))
        {
            parsedIndustryIds = request.IndustryIds.Split(',')
                .Select(id => Guid.TryParse(id.Trim(), out var guid) ? guid : (Guid?)null)
                .Where(guid => guid.HasValue)
                .Select(guid => guid!.Value)
                .ToList();

            if (!parsedIndustryIds.Any())
            {
                throw new MyBadRequestException("Lĩnh vực không hợp lệ");
            }
        }

        List<int>? parseNOEs = null;
        if (!string.IsNullOrEmpty(request.NumberOfEmployees))
        {
            parseNOEs = request.NumberOfEmployees.Split(',')
                .Select(id => int.TryParse(id.Trim(), out var number) ? number : (int?)null)
                .Where(number => number.HasValue)
                .Select(number => number!.Value)
                .ToList();

            if (!parseNOEs.Any())
            {
                throw new MyBadRequestException("Lựa chọn quy mô không hợp lệ");
            }
        }

        List<int> parseNOYs = null;
        if (!string.IsNullOrEmpty(request.NOYEstablisheds))
        {
            parseNOYs = request.NOYEstablisheds.Split(',')
                .Select(id => int.TryParse(id.Trim(), out var number) ? number : (int?)null)
                .Where(number => number.HasValue)
                .Select(number => number!.Value)
                .ToList();

            if (!parseNOYs.Any())
            {
                throw new MyBadRequestException("Lựa chọn số năm hoạt động không hợp lệ");
            }
        }

        var result = await _businessRepository.SearchBusinessAsync(request, parsedIndustryIds!, parseNOEs, parseNOYs);

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
