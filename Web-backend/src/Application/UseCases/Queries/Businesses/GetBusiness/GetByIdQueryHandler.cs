using Application.Abstractions.Data;
using AutoMapper;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.Business.GetById;
using Contract.Services.Business.Share;
using Domain.Abstractioins.Exceptions;

namespace Application.UseCases.Queries.Businesses.GetBusiness;
public class GetByIdQueryHandler(IBusinessRepository _businessRepository, IMapper _mapper) : IQueryHandler<GetByIdQuery, BusinessResponse>
{
    public async Task<Result.Success<BusinessResponse>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _businessRepository.GetByIdAsync(request.Id)
            ?? throw new MyNotFoundException("Không tìm thấy doanh nghiệp");

        var businessResponse = _mapper.Map<BusinessResponse>(result);   

        return Result.Success<BusinessResponse>.Get(businessResponse);
    }
}
