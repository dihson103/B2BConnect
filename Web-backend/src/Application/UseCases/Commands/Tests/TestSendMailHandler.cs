using Application.Abstractions.Services;
using Application.Utils;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.Event.Create;
using Contract.Services.Tests;
using Domain.Entities;

namespace Application.UseCases.Commands.Tests;
internal class TestSendMailHandler(IEmailService emailService)
    : ICommandHandler<TestSendMail>
{
    public async Task<Result.Success> Handle(TestSendMail request, CancellationToken cancellationToken)
    {
        var e = Event.Create(new CreateEventCommand(
            "Sự kiện test", 
            "Hồ Chí Minh",
            DateTime.Now, 
            DateTime.Now.AddDays(1),
            "Hồ Chí Minh",
            null, null), "00932442");
        var isSuccess = await emailService.SendEmailAsync(request.toEmail, EmailTemplateUtl.GetEventBody(e));

        var message = isSuccess ? "Gửi email thành công" : "Gửi email thất bại";

        return Result.Success.Create(message);
    }
}
