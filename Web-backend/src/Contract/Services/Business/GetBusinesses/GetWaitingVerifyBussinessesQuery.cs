using Contract.Abstractions.Dtos.Search;
using Contract.Abstractions.Messages;
using Contract.Services.Business.Share;

namespace Contract.Services.Business.GetBusinesses;
public record GetWaitingVerifyBussinessesQuery(
    string? SearchTerm,
    bool newestSorting = true,
    int PageIndex = 1,
    int PageSize = 10) : IQuery<SearchResponse<List<BusinessWaitingVerifyResponse>>>;

