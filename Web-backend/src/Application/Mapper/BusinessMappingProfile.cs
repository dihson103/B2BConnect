using AutoMapper;
using Contract.Services.Branch.Share;
using Contract.Services.Business.Share;
using Contract.Services.Representative.Share;
using Domain.Entities;

namespace Application.Mapper;
public class BusinessMappingProfile : Profile
{
    public BusinessMappingProfile()
    {
        CreateMap<Business, BusinessResponse>()
             .ConstructUsing(src => new BusinessResponse(
                src.Id,
                src.TaxCode,
                src.Name,
                src.DateOfEstablishment,
                src.WebSite,
                src.AvatarImage,
                src.CoverImage,
                src.NumberOfEmployee,
                src.IsVerified,
                new RepresentativeResponse(
                        src.Representative!.Id,
                        src.Representative.GovernmentId,
                        src.Representative.Fullname,
                        src.Representative.Dob,
                        src.Representative.Gender,
                        src.Representative.Address,
                        src.Representative.BusinessId),
               src.Branches!.Select(branch => new BranchResponse(
                        branch.Id,
                        branch.Email,
                         branch.Phone,
                        branch.Address,
                        branch.IsMainBranch,
                        branch.BusinessId)).ToList()
            ));
    }

}
