using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using TourGuide.Api.Web.Abstractions;

namespace TourGuide.Api.Web.Services;

public class EmailService : IEmailService
{
    private readonly SmtpClient _client;

    public EmailService(IOptions<EmailOptions> options)
    {
        _client = new SmtpClient();

        _client.Host = options.Value.Host;
        _client.Port = options.Value.Port;
        _client.EnableSsl = true;
        _client.Credentials = new NetworkCredential(
            options.Value.Login,
            options.Value.Password);
    }
    
    public async Task SendAsync(string to, string subject, string body)
    {
        var from = new MailAddress(_client.Credentials!.GetCredential(_client.Host!, _client.Port, "")!.UserName);

        var addressTo = new MailAddress(to);

        var message = new MailMessage(from, addressTo);

        message.Body = body;
        message.Subject = subject;

        await _client.SendMailAsync(message);
    }
}