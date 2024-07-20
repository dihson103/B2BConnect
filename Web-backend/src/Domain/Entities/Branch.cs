using Contract.Services.Branch.Create;
using Contract.Services.Branch.Update;
using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Branch : EntityBase<Guid>
{
    public string? Email { get; set; }
    public string? Phone {  get; set; }
    public string Address { get; set; }
    public bool IsMainBranch { get; set; } = false;
    public Guid BusinessId { get; set; }
    public Business Business { get; set; }
    private Branch()
    {
    }

    public static Branch Create(CreateBranchCommand branch)
    {
        return new Branch()
        {
            Id = Guid.NewGuid(),
            Email = branch.Email,
            Phone = branch.Phone,
            Address = branch.Address,
            IsMainBranch = branch.IsMainBranch,
            BusinessId = branch.BusinessId
        };
    }

    public void Update(UpdateBranchRequest branchRequest)
    {
        Email = branchRequest.Email;
        Phone = branchRequest.Phone;
        Address = branchRequest.Address;
        IsMainBranch = !branchRequest.IsMainBranch;
    }
}
