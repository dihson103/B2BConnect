namespace Application.Abstractions.Services;
public interface IRequestContext
{
    string UserLoggedIn { get; }

    bool IsAdminLogged { get; }
}
