using Application.Abstractions.Data;
using Application.Abstractions.Services;
using Application.UseCases.Queries.Accounts.Login;
using Contract.Services.Account.Login;
using Domain.Abstractioins.Exceptions;
using Domain.Entities;
using Moq;

namespace Application.UnitTests.Queries.Accounts;

public class LoginAccountCommandHandlerTest
{
    private readonly LoginAccountCommandHandler _handler;
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    private readonly Mock<IPasswordService> _passwordServiceMock;
    private readonly Mock<IJwtService> _jwtServiceMock;
    public LoginAccountCommandHandlerTest()
    {
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _passwordServiceMock = new Mock<IPasswordService>();
        _jwtServiceMock = new Mock<IJwtService>();
        _handler = new LoginAccountCommandHandler(
            _accountRepositoryMock.Object, 
            _passwordServiceMock.Object, 
            _jwtServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrowMyUnauthorizedException_WhenWrongEmail() {
        var loginCommand = new LoginCommand("wrongEmail", "password");
        _accountRepositoryMock.Setup(s => s.LoginAsync(It.IsAny<string>())).ReturnsAsync((Account) null);
        await Assert.ThrowsAsync<MyUnauthorizedException>(async () => {
           var result = await _handler.Handle(loginCommand, default);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowMyUnauthorizedException_WhenWrongPassword() {
         var loginCommand = new LoginCommand("wrongEmail", "password");
        _passwordServiceMock.Setup(s => s.IsVerify(It.IsAny<string>(), It.IsAny<string>())).Returns(null);
        await Assert.ThrowsAsync<MyUnauthorizedException>(async () => {
           var result = await _handler.Handle(loginCommand, default);
        });
    }
}