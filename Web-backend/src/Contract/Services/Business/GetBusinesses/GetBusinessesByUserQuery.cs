using Contract.Abstractions.Dtos.Search;
using Contract.Abstractions.Messages;
using Contract.Services.Business.Share;

namespace Contract.Services.Business.GetBusinesses;
public record GetBusinessesByUserQuery(
    string? SearchTerm,
     string? IndustryIds,
    bool? IsVerified,
    string? NumberOfEmployees,
    string? NOYEstablisheds,
    int PageIndex = 1,
    int PageSize = 10) : IQuery<SearchResponse<List<BusinessesResponse>>>;
