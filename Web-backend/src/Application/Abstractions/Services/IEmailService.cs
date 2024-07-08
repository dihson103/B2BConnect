namespace Application.Abstractions.Services;
public interface IEmailService
{
    Task<bool> SendEmailAsync(string toMail, string html);
}
