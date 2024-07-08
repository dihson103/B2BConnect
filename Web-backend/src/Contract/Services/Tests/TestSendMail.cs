using Contract.Abstractions.Messages;

namespace Contract.Services.Tests;
public record TestSendMail(string toEmail) : ICommand;
