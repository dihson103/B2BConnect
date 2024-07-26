using Contract.Abstractions.Dtos.Search;
using Contract.Abstractions.Messages;
using Contract.Services.Business.Share;

namespace Contract.Services.Business.GetBusinesses;
public record GetBusinessesByUserQuery(
    string? SearchTerm,
   List<Guid>? IndustryIds,
   bool? IsVerified,
    NumberOfEmployee? NumberOfEmployee,
    NumberOfYearEstablished? NOYEstablished,
    int PageIndex = 1,
    int PageSize = 10) : IQuery<SearchResponse<List<BusinessesResponse>>>;
