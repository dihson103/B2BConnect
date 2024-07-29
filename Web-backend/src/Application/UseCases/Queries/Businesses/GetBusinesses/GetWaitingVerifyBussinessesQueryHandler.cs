using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Services;
using AutoMapper;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Dtos.Search;
using Contract.Abstractions.Messages;
using Contract.Services.Business.GetBusinesses;
using Contract.Services.Business.Share;
using Domain.Abstractioins.Exceptions;

namespace Application.UseCases.Queries.Businesses.GetBusinesses;
public sealed class GetWaitingVerifyBussinessesQueryHandler(IBusinessRepository _businessRepository, IMapper _mapper
    , IRequestContext _requestContext) :
    IQueryHandler<GetWaitingVerifyBussinessesQuery, SearchResponse<List<BusinessWaitingVerifyResponse>>>
{
    public async Task<Result.Success<SearchResponse<List<BusinessWaitingVerifyResponse>>>> Handle(GetWaitingVerifyBussinessesQuery request, CancellationToken cancellationToken)
    {
        var result = await _businessRepository.SearchWaitingBusinessAsync(request);

        var businesses = result.Item1;
        var totalPage = result.Item2;
        var totalItems = result.Item3;

        if (businesses is null || businesses.Count <= 0 || totalPage <= 0)
        {
            return Result.Success<SearchResponse<List<BusinessWaitingVerifyResponse>>>
               .Get(null, "Không tìm thấy doanh nghiệp chờ xác thực nào phù hợp");
        }

        var searchResponse = new SearchResponse<List<BusinessWaitingVerifyResponse>>(request.PageIndex, totalPage, totalItems, businesses);

        return Result.Success<SearchResponse<List<BusinessWaitingVerifyResponse>>>.Get(searchResponse);
    }
}
