using Contract.Services.Account.GetAccounts;
using Domain.Entities;

namespace Application.Abstractions.Data;
public interface IAccountRepository
{
    Task<Account?> GetAccountByIdAsync(Guid id);
    Task<Account?> GetAccountActiveByIdAsync(Guid id);
    Task<bool> IsAccountExistAsync(Guid id);
    void AddAccount(Account account);
    Task<(List<Account>?, int)> SearchAccountsAsync(GetAccountsQuery request);
    void Update(Account account);
    Task<Account?> LoginAsync(string email);
}
