using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Branch : EntityBase<Guid>
{
    public string Email { get; set; }
    public string Phone {  get; set; }
    public string Address { get; set; }
    public bool IsMainBranch { get; set; }  
    public string BusinessId { get; set; }
    public Business Business { get; set; }
    private Branch()
    {
    }
}
