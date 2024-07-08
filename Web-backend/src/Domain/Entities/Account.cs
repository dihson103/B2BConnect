using System.ComponentModel.DataAnnotations.Schema;
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

    public static Account Create(CreateAccountCommand request)
    {
        return new Account
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            IsActive = true,
            IsAdmin = false,
            // Password = hashPassword,
        };
    }

}
