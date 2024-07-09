using Application.Abstractions.Data;
using Application.Abstractions.Services;
using Application.UseCases.Commands.Accounts.Create;
using Contract.Services.Account.Create;
using Domain.Abstractioins.Exceptions;
using FluentValidation;
using Moq;

namespace Application.UnitTests.Commands.Accounts;

public class CreateAccountCommandHandlerTest 
{
    private readonly CreateAccountCommandHandler _handler;
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    private readonly Mock<IPasswordService> _passwordServiceMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly IValidator<CreateAccountCommand> _validator;

    public CreateAccountCommandHandlerTest() {
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _passwordServiceMock = new Mock<IPasswordService>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _validator = new CreateAccountValidator();
        _handler = new CreateAccountCommandHandler(
            _accountRepositoryMock.Object, 
            _passwordServiceMock.Object, 
            _unitOfWorkMock.Object,
            _validator);
    }

    [Fact]
    public async Task Handler_ShouldThrowException_WhenFieldEmpty()
    { 
        var newAccount = new CreateAccountCommand("", "");
        
        await Assert.ThrowsAsync<MyValidationException>(async () => {
            var result = await _handler.Handle(newAccount, default);
        });
    }

    [Fact]
    public async Task Handler_ShouldThrowException_WhenEmailIsNotTheRightFormat()
    { 
        var newAccount = new CreateAccountCommand("password", "email");
        
        await Assert.ThrowsAsync<MyValidationException>(async () => {
            var result = await _handler.Handle(newAccount, default);
        });
    }

    [Fact]
    public async Task Handler_ShouldCreateNewAccount_WhenSucess()
    { 
        var newAccount = new CreateAccountCommand("passw", "test@gmail.com");
        _passwordServiceMock.Setup(s => s.Hash(It.IsAny<string>())).Returns("hashPassword");
        var result = await _handler.Handle(newAccount, default);
    }
}
