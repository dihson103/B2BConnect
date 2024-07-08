using AutoMapper;
using Contract.Services.Branch.Share;
using Domain.Entities;

namespace Application.Mapper;
public class BranchMappingProfile: Profile
{
    public BranchMappingProfile()
    {
        CreateMap<Branch, BranchResponse>()
            .ConstructUsing(src => new BranchResponse(
               src.Id,
               src.Email,
               src.Phone,
               src.Address,
               src.IsMainBranch,
               src.BusinessId
           ));
    }
}
