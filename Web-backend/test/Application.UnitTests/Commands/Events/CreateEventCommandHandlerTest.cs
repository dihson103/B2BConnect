using System.Reflection.Metadata;
using Application.Abstractions.Data;
using Application.UseCases.Commands.Events.Create;
using Contract.Services.Event.Create;
using Contract.Services.Event.Share;
using Domain.Abstractioins.Exceptions;
using FluentValidation;
using Moq;

namespace Application.UnitTests.Commands.Events;
public class CreateEventCommandHandlerTest
{
    private readonly IValidator<CreateEventCommand> validator;
    private readonly Mock<IEventRepository> eventRepositoryMock;
    private readonly Mock<IUnitOfWork> unitOfWorkMock;
    public CreateEventCommandHandlerTest()
    {
        eventRepositoryMock = new Mock<IEventRepository>();
        unitOfWorkMock = new Mock<IUnitOfWork>();   
        validator = new CreateEventValidator(eventRepositoryMock.Object);
    }

    [Fact] //Theory
    public async Task HandlerShouldThrowMyValidationException_WhenNameEmpty()
    {
        var request = new CreateEventCommand("ffff", "Description", DateTime.UtcNow.AddDays(3), DateTime.UtcNow, EventStatus.PLANNING);
        var handler = new CreateEventCommandHandler(
            eventRepositoryMock.Object,
            unitOfWorkMock.Object,
            validator);

        await Assert.ThrowsAsync<MyValidationException>(async () =>
        {
            await handler.Handle(request, default);
        });
    }

}
