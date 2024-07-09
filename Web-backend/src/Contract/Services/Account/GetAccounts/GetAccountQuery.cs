using Contract.Abstractions.Messages;
using Contract.Abstractions.Dtos.Search;
using Contract.Services.Account.SharedDto;

namespace Contract.Services.Account.GetAccounts;

public record GetAccountsQuery(
    string SearchTerm,
    int PageIndex = 1,
    int PageSize = 10) : IQuery<SearchResponse<List<AccountResponse>>>;
