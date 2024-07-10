using Application.Abstractions.Data;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.Event.RequestJoin;
using Domain.Abstractioins.Exceptions;
using Domain.Entities;

namespace Application.UseCases.Commands.Events.RequestJoin;
internal sealed class RequestJoinCommandHanlder(
    IEventRepository _eventRepository,
    IBusinessRepository _businessRepository,
    ISectorRepository _sectorRepository,
    IEventIndustryRepository _eventIndustryRepository,
    IParticipationRepository _participationRepository,

    IUnitOfWork _unitOfWork) : ICommandHandler<RequestJoinCommand>
{
    public async Task<Result.Success> Handle(RequestJoinCommand request, CancellationToken cancellationToken)
    {
        var isBusinessValid = await _businessRepository.IsBusinessValidAsync(request.BusinessId);
        if (!isBusinessValid)
        {
            throw new MyNotFoundException($"Không tìm thấy công ty có id: {request.BusinessId}");
        }

        var isEventValidToJoin = await _eventRepository.IsEventValidToJoinAsync(request.EventId);
        if (!isEventValidToJoin)
        {
            throw new MyNotFoundException("Không tìm thấy sự kiện hoặc sự kiện không còn nhận yêu cầu tham gia");
        }

        var isBusinessRequested = await _participationRepository.IsBusinessRequestedAsync(request.BusinessId, request.EventId);
        if (isBusinessRequested)
        {
            throw new MyBadRequestException("Công ty đã gửi yêu cầu tham gia sự kiện");
        }

        var sectors = await _sectorRepository.GetSectorsByBusinessIdAsync(request.BusinessId);
        if(sectors is null || sectors.Count == 0) throw new MyNotFoundException("Không tìm thấy lĩnh vực của công ty");
        var industryIds = sectors.Select(s => s.IndustryId).ToList();
        var isBusinessSectorsInEventIndustries = await _eventIndustryRepository.IsInEventIndustriesAsync(industryIds, request.EventId);

        var participation = Participation.Create(request.BusinessId, request.EventId, isBusinessSectorsInEventIndustries);
        _participationRepository.Add(participation);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success.Create(isBusinessSectorsInEventIndustries ? "Tham gia sự kiện thành công" : "Gửi yêu cầu tham gia sự kiện thành công");
    }
}
