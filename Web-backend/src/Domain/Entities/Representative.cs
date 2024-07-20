using Contract.Services.Representative.Create;
using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Representative : EntityBase<Guid>
{
    public string GovernmentId { get; set; }
    public string Fullname { get; set; }
    public DateOnly Dob { get; set; }
    public bool Gender { get; set; } = true;
    public string Address { get; set; }

    public Guid BusinessId { get; set; }

    public Business Business { get; set; }
    private Representative()
    {
    }

    public static Representative Create(CreateRepresentativeCommand command)
    {
        
        return new Representative()
        {
            Id = Guid.NewGuid(),
            GovernmentId = command!.GovernmentId,
            Fullname = command.Fullname,
            Dob = command.Dob,
            Gender = command.Gender,
            Address = command.Address,
        };
    }

    public void Update(CreateRepresentativeCommand command)
    {
        GovernmentId = command.GovernmentId;
        Fullname = command.Fullname;
        Dob = command.Dob;
        Gender = command.Gender;
        Address = command.Address;
    }
}
