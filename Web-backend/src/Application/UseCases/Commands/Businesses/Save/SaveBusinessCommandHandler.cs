using Application.Abstractions.Data;
using Application.Abstractions.Services;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.Branch.Create;
using Contract.Services.Business.Create;
using Domain.Abstractioins.Exceptions;
using Domain.Entities;
using FluentValidation;

namespace Application.UseCases.Commands.Businesses.Create;
public class SaveBusinessCommandHandler(IBusinessRepository _businessRepository, IRequestContext _context,
    IUnitOfWork _unitOfWork, IBranchRepository _branchRepository, ISectorRepository _sectorRepository,
    IRepresentativeRepository _representativeRepository, IAccountRepository _accountRepository,
    IValidator<SaveBusinessCommand> _businessValidator, IValidator<CreateBranchCommand> _branchValidator)
    : ICommandHandler<SaveBusinessCommand>
{
    public async Task<Result.Success> Handle(SaveBusinessCommand request, CancellationToken cancellationToken)
    {
        await ValidateBusinessAsync(request!);

        await ValidateBranchAsync(request.Branches);
        var loggedUser = _context.UserLoggedIn ?? throw new MyBadRequestException("Tài khoản chưa đăng nhập");

        var isAdminLogged = _context.IsAdminLogged;
        var branches = new List<Branch>();
        var businesIndustry = new List<Sector>();

        var account = await _accountRepository.GetAccountByIdAsync(request.accountId)
                    ?? throw new MyBadRequestException("account không tồn tại");

        var business = await _businessRepository.getByAccountIdAsync(account.Id);

        if (business == null)
        {
            business = Business.Create(request!, loggedUser);
            business.AccountId = account.Id;
            _businessRepository.Add(business);

            if (request!.RepresentativeSave != null)
            {
                var re = Representative.Create(request!.RepresentativeSave!);
                re.BusinessId = business.Id;
                _representativeRepository.Add(re);
            }

            businesIndustry = request!.IndustryIds.Select(i => Sector.Create(business.Id, i)).ToList();

            if (request.Branches != null)
            {
                foreach (var b in request.Branches)
                {
                    var branchCommand = new CreateBranchCommand(b.Email, b.Phone, b.Address, b.IsMainBranch);
                    var branch = Branch.Create(branchCommand);
                    branch.BusinessId = business.Id;
                    branches.Add(branch);
                }
            }
        }
        else
        {
            if (isAdminLogged || business.AccountId == new Guid(loggedUser))
            {
                business.Update(request, loggedUser);
                _businessRepository.Update(business);

                if (request!.RepresentativeSave != null)
                {
                    _representativeRepository.DeleteByBusinessId(business!.Id);
                    var re = Representative.Create(request.RepresentativeSave!);
                    re.BusinessId = business.Id;
                    _representativeRepository.Add(re);
                }

                if (request.Branches != null)
                {
                    _branchRepository.DeleteByBusinessId(business.Id);
                    foreach (var b in request.Branches)
                    {
                        var branchCommand = new CreateBranchCommand(b.Email, b.Phone, b.Address, b.IsMainBranch);
                        var branch = Branch.Create(branchCommand);
                        branch.BusinessId = business.Id;
                        branches.Add(branch);
                    }
                }

                _sectorRepository.DeleteByBusinessId(business.Id);
                businesIndustry = request!.IndustryIds.Select(i => Sector.Create(business.Id, i)).ToList();
            }
        }

        if (businesIndustry.Count > 0)
        {
            _sectorRepository.AddRange(businesIndustry);
        }

        if (branches.Count > 0)
        {
            _branchRepository.AddRange(branches);
        }

        await _unitOfWork.SaveChangesAsync();
        return Result.Success.Update("Cập nhật doanh nghiệp thành công");
    }

    private async Task ValidateBusinessAsync(SaveBusinessCommand command)
    {
        var validatorResult = await _businessValidator.ValidateAsync(command);
        if (!validatorResult.IsValid)
        {
            throw new MyValidationException(validatorResult.ToDictionary());
        }
    }

    private async Task ValidateBranchAsync(List<CreateBranchCommand> commands)
    {
        var validationResults = new List<FluentValidation.Results.ValidationResult>();

        foreach (var command in commands)
        {
            var result = await _branchValidator.ValidateAsync(command);
            if (!result.IsValid)
            {
                validationResults.Add(result);
            }
        }

        if (validationResults.Any())
        {
            var failures = validationResults
                .SelectMany(result => result.Errors)
                .ToList();

            var failureDictionary = failures
                .GroupBy(f => f.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(f => f.ErrorMessage).ToArray()
                );

            throw new MyValidationException(failureDictionary);
        }

    }
}
