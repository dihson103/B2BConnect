using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using Contract.Services.Account.Create;
using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Account : EntityBase<Guid>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsAdmin { get; set; } = false;
    public Business Business { get; set; }
    private Account()
    {
    }

    public static Account Create(string email, string hashPassword)
    {
        return new Account
        {
            Id = Guid.NewGuid(),
            Email = email,
            IsActive = true,
            IsAdmin = false,
            Password = hashPassword,
        };
    }

}
