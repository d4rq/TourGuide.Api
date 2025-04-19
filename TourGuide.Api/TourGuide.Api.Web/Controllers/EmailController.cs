using Microsoft.AspNetCore.Mvc;
using TourGuide.Api.Web.Abstractions;

namespace TourGuide.Api.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    [HttpPost("send")]
    public async Task Send(
        [FromServices] IEmailService emailService,
        [FromForm] string to)
    {
        await emailService.SendAsync(to, "Привет!", "Привет!");
    }
}