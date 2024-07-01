using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Branch : EntityBase<int>
{
    public string? Email { get; set; }
    public string? Phone {  get; set; }
    public string Address { get; set; }
    public bool IsMainBranch { get; set; } = false;
    public int BusinessId { get; set; }
    public Business Business { get; set; }
    private Branch()
    {
    }
}
