using System.Net;
using System.Net.Mail;
using Application.Abstractions.Services;

namespace Infrastructure.Services;
internal class EmailService : IEmailService
{
    public async Task<bool> SendEmailAsync(string toMail, string html)
    {
        string fromEmail = "sonndhe160021@fpt.edu.vn";
        string password = "otui jrtf hskq ehpz";

        try
        {
            MailMessage message = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = "Test send mail"
            };
            message.To.Add(new MailAddress(toMail));
            message.Body = html;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, password),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(message);
            Console.WriteLine("Email sent successfully.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
            return false;
        }
    }

}
