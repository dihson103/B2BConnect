using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Representative : EntityBase<string>
{
    public string Fullname { get; set; }
    public DateOnly Dob {  get; set; }
    public bool Gender { get; set; }
    public string Nationality { get; set; }
    public string Address { get; set; }
    public Business Business { get; set; }  
    private Representative()
    {
    }
}
