using AutoMapper;
using Contract.Services.Account.SharedDto;
using Contract.Services.Branch.Share;
using Contract.Services.Business.Share;
using Contract.Services.Representative.Share;
using Domain.Entities;

namespace Application.Mapper.Businesses;
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
                 src.Account != null ? new AccountResponse(
                        src.Account.Email,
                        src.Account!.Id,
                        src.Account.IsAdmin
                       ) : null,
                src.Representative != null ? new RepresentativeResponse(
                        src.Representative!.Id,
                        src.Representative.GovernmentId,
                        src.Representative.Fullname,
                        src.Representative.Dob,
                        src.Representative.Gender,
                        src.Representative.Address,
                        src.Representative.BusinessId) : null,
                src.Branches != null ? src.Branches!.Select(branch => new BranchResponse(
                        branch.Id,
                        branch.Email,
                         branch.Phone,
                        branch.Address,
                        branch.IsMainBranch,
                        branch.BusinessId)).ToList() : null
            ));
    }

}
