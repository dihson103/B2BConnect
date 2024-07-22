using Contract.Abstractions.Dtos.Search;
using Contract.Abstractions.Messages;
using Contract.Services.Business.Share;

namespace Contract.Services.Business.GetBusinesses;
public record GetBusinessesByAdminQuery
    (string? SearchTerm,
    bool IsVerified = false,
    int PageIndex = 1,
    int PageSize = 10) : IQuery<SearchResponse<List<BusinessesResponse>>>;

