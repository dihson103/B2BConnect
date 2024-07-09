using System.Diagnostics.Tracing;
using Application.Abstractions.Data;
using Application.Abstractions.Services;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.Account.Create;
using Contract.Services.Event.Create;
using Domain.Abstractioins.Exceptions;
using Domain.Entities;
using FluentValidation;

namespace Application.UseCases.Commands.Accounts.Create;
public class CreateAccountCommandHandler(
    IAccountRepository _accountRepository,
    IPasswordService _passwordService,
    IUnitOfWork _unitOfWork,
    IValidator<CreateAccountCommand> _validator) : ICommandHandler<CreateAccountCommand>
{
    public async Task<Result.Success> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        await ValidateRequestAndGetAccount(request);

        var hashPassword = _passwordService.Hash(request.Password);

        var account = Account.Create(request.Email, hashPassword);
        _accountRepository.AddAccount(account);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success.Create("Tạo tài khoản thành công.");
    }

    private async Task ValidateRequestAndGetAccount(CreateAccountCommand request)
    {
        var validatorResult = await _validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            throw new MyValidationException(validatorResult.ToDictionary());
        }
    }
}
