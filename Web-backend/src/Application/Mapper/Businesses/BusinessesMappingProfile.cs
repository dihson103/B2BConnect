using AutoMapper;
using Contract.Services.Business.Share;
using Domain.Entities;

namespace Application.Mapper.Businesses;
public class BusinessesMappingProfile : Profile
{
    public BusinessesMappingProfile()
    {
        CreateMap<Business, BusinessesResponse>()
             .ConstructUsing(src => new BusinessesResponse(
                src.Id,
                src.TaxCode,
                src.Name,
                src.DateOfEstablishment,
                src.WebSite,
                src.AvatarImage,
                src.CoverImage,
                src.NumberOfEmployee.ToString(),
                src.IsVerified
            ));
    }

}
