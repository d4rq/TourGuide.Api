namespace TourGuide.Api.Web.Abstractions;

public interface IEmailService
{
    Task SendAsync(string to, string subject, string body);
}