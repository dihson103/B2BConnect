using Application.Abstractions.Data;
using Contract.Services.Account.GetAccounts;
using Contract.Services.Event.GetEvents;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;
internal class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddAccount(Account account)
    {
        _context.Accounts.Add(account);
    }

    public async Task<Account?> GetAccountByIdAsync(Guid id)
    {
        return await _context.Accounts
            .SingleOrDefaultAsync(account => account.Id == id);
    }

    public async Task<bool> IsAccountExistAsync(Guid id)
    {
        return await _context.Accounts.AnyAsync(account => account.Id == id);
    }
        public Task<Account?> GetAccountActiveByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<(List<Account>?, int)> SearchAccountsAsync(GetAccountsQuery request)
    {
        throw new NotImplementedException();
    }

    public void Update(Account account)
    {
        throw new NotImplementedException();
    }

    public async Task<Account?> LoginAsync(string email)
    {
        var account = await _context.Accounts.SingleOrDefaultAsync(account => account.Email == email && account.IsActive == true);
        return account;
    }
}
