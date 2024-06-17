namespace Application.Abstractions.Services;
public interface IPasswordService
{
    string Hash(string password);
    bool IsVerify(string hashPassword, string password);
}
